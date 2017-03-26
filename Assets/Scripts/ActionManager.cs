using UnityEngine;
using System.Collections;
using System.Linq;

public delegate void TargetedAction(UnitActionBase action, Transform target);
public delegate void ActionEventHandler(UnitActionBase action);
public delegate void ActionManagerEventHander(ActionManager mngr);
public delegate void ActionTargetEventHandler(object target);

public class ActionManager : MonoBehaviour {

    Unit m_Owner;
    
    public int AP_Used = 2;
    public  int MaxAP = 2;
    public int CurrentTurnCost = 0;

    public void RestCharges()
    {
        foreach(UnitActionBase action in Actions)
        {
            action.ChargeController.ResetCharge();
        }
    }
    public static ActionManagerEventHander OnActionManagerActivated;
    
    public int GetOwnerID()
    {
        return m_Owner.OwnerID;
    }
   
    public UnitActionBase[] Actions;
    UnitActionBase currentAction;

    public TargetedAction OnTargetAction;
    public ActionEventHandler OnActionSelected;
    public ActionEventHandler OnActionUnselected;
    public ActionEventHandler OnActionComplete;
    public ActionEventHandler OnActionStarted;

    public UnitActionBase GetCurrentAction()
    {
        return currentAction;
    }

    public bool IsActionInProgress
    {
        get
        {
            foreach (UnitActionBase action in Actions) 
                if(action.ActionInProgress) return true;

             return false;
         }           
    }


    void CheckRemoved(Unit u)
    {
        if(u == m_Owner)
        {
            Reset();
            Unit.OnUnitKilled -= CheckRemoved;
            Unit.OnEvacuated -= CheckRemoved;
        }
    }
	void Start(){

        Unit.OnUnitKilled += CheckRemoved;
        Unit.OnEvacuated += CheckRemoved;

		UnitActionBase[] raw_actions = GetComponentsInChildren<UnitActionBase>();
		foreach (UnitActionBase action in raw_actions) action.SetOwner(m_Owner);

        Actions = raw_actions;
		

	}
    public Unit GetOwner()
    {
        return m_Owner;
    }
    public void SetOwner(Unit owner)
    {
        m_Owner = owner;
        Unit.OnTurnEnded += unit =>
        {
            if(unit == owner)
                Reset();
        };

        Unit.OnTurnStart += unit =>
        {
            if (unit == owner) { 
                AP_Used = 0;
                if (OnActionManagerActivated != null) OnActionManagerActivated(this);
            }
        };      
    }


    public int GetAPLeft()
    {
       // MDebug.Log((MaxAP - AP_Used).ToString());
        return (MaxAP - AP_Used);
    }

    public void Reset()
    {
       //hello world MDebug.Log("Resetting action manager");
        UnsetCurrentAction();
       
        CurrentTurnCost = 0;
    }
    public bool HasAP()
    {
        return HasAP(1);
    }
    public bool HasAP(int ap)
    {
        bool r = GetAPLeft() >= ap;
        if (!r)
        {
          //  MDebug.Log("not enough AP");
        }
        return r;
    }

    public void SkipTurn()
    {       
        MDebug.Log("Skip");
        AP_Used = MaxAP;
        CurrentTurnCost = 5;
    }

    public void Evacuate()
    {
        MDebug.Log("^unit EVAC");
        if(!GetOwner().Evacuate()){
            ToastNotification.SetToastMessage2("Must be in Evacuation zone");
        }
    }

    void Update()
    {
        //Only Read Inputs if this is the currently active actionmanager
        if ( m_Owner.OwnerID != 0) return;

        if (TurnSystem.Instance == null || !TurnSystem.HasTurn(m_Owner))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
             SkipTurn();
             return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(currentAction != null &&  GetOwnerID() == 0)
                SoundManager.PlayAtSourceTag("SoundSFXSource", "sfx_" + SoundManager.Presets.ability_cancel.ToString());
            UnsetCurrentAction();
        }
       
    }


    public UnitActionBase SelectAbility(UnitActionBase ability)
    {

        if (Unit.SelectedUnit != m_Owner || !HasAP() || IsAnimationPlaying() || m_Owner.IsDead())
        {
            MDebug.Log("^abilityCould not select ability " + ability.GetActionID());
            return null;
        }
        if (currentAction != null && currentAction == ability)
        {
            if (GetOwnerID() == 0)
                SoundManager.PlayAtSourceTag("SoundSFXSource", "sfx_" + SoundManager.Presets.ability_cancel.ToString());
            UnsetCurrentAction();
            return null;
        }
 

        UnsetCurrentAction();
        if (!ability.CanExecAction(true))
        {
            MDebug.Log("^abilitycan not execute ability " + ability.GetActionID());
            if (GetOwnerID() == 0)
                SoundManager.PlayAtSourceTag("SoundSFXSource", "sfx_" + SoundManager.Presets.error.ToString());
            return null;
        }
        ToastNotification.StopToast();

        currentAction = ability;
        currentAction.OnActionStart += OnActionUsed;
        currentAction.OnActionComplete += onComplete;
        currentAction.OnTarget += TargetedAction;
        currentAction.SelectAction();
        if(GetOwnerID() == 0)
            SoundManager.PlayAtSourceTag("SoundSFXSource", "sfx_" + SoundManager.Presets.ability_select.ToString());

        if (OnActionSelected != null) OnActionSelected(currentAction);
        return currentAction;

    }

    private void UnsetCurrentAction()
    {
        if (currentAction == null) return;

        currentAction.UnSelectAction();
        currentAction.OnActionStart -= OnActionUsed;
        
        currentAction.OnTarget -= TargetedAction;
        currentAction = null;
       
        if (OnActionUnselected != null) OnActionUnselected(currentAction);
   

    }

    void onComplete(UnitActionBase action)
    {
        OnActionComplete(action);
        action.OnActionComplete -= onComplete;
    }
    void OnActionUsed(UnitActionBase action)
    {
        AP_Used += action.GetEndsTurn() ? MaxAP : action.AP_Cost;

        CurrentTurnCost += (int) action.GetTimeCost();

      //  if (OnActionComplete != null) OnActionComplete(action);
      
        if (TurnSystem.HasTurn(m_Owner) && RC_Camera.Instance != null) {
            RC_Camera.Instance.ActionPanToPos.GoTo(m_Owner.currentTile.GetPosition());
        }

        UnsetCurrentAction();
        if (OnActionStarted != null) OnActionStarted(action);

        // MDebug.Log(Owner.GetID() + " Action used" + action.ActionID);
       
    }

    void TargetedAction(UnitActionBase b, Transform tgt)
    {
       // MDebug.Log("targeted action");
        if(OnTargetAction != null)
        {
            OnTargetAction(b, tgt);
        }

        if(GetOwnerID() == 0)
        {
            SoundManager.PlayAtSourceTag("SoundSFXSource", "sfx_" + SoundManager.Presets.ability_confirm);
        }
    }

    public T GetActionOfType <T>()
    {
        return Actions.OfType<T>().ToList()[0];
    }

    bool IsAnimationPlaying()
    {
        foreach(var v in Actions)
        {
            if (v.ActionInProgress)
                return true;
        }

        return false;
    }

}
