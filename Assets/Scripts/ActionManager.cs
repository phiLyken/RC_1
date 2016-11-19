using UnityEngine;
using System.Collections;
using System.Linq;

public delegate void TargetedAction(UnitActionBase action, Transform target);
public delegate void ActionEventHandler(UnitActionBase action);
public delegate void ActionManagerEventHander(ActionManager mngr);
public delegate void ActionTargetEventHandler(object target);

public class ActionManager : MonoBehaviour {

    Unit Owner;
    
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
        return Owner.OwnerID;
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


    void CheckOwnerKilled(Unit u)
    {
        if(u == Owner)
        {
            Reset();
            Unit.OnUnitKilled -= CheckOwnerKilled;
        }
    }
	void Start(){

        Unit.OnUnitKilled += CheckOwnerKilled;
		UnitActionBase[] raw_actions = GetComponentsInChildren<UnitActionBase>();
		foreach (UnitActionBase action in raw_actions) action.SetOwner(Owner);

        Actions = raw_actions;
		

	}
    public void SetOwner(Unit owner)
    {
        Owner = owner;
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
       // Debug.Log((MaxAP - AP_Used).ToString());
        return (MaxAP - AP_Used);
    }

    public void Reset()
    {
     //   Debug.Log("Resetting action manager");
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
          //  Debug.Log("not enough AP");
        }
        return r;
    }

    public void SkipTurn()
    {       
        Debug.Log("Skip");
        AP_Used = MaxAP;
        CurrentTurnCost = 5;
    }

    void Update()
    {
        //Only Read Inputs if this is the currently active actionmanager
        if ( Owner.OwnerID != 0) return;

        if (TurnSystem.Instance == null || !TurnSystem.HasTurn(Owner))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
             SkipTurn();
             return;
        }        

       
    }


    public UnitActionBase SelectAbility(UnitActionBase ability)
    {

        if (Unit.SelectedUnit != Owner || !HasAP() || IsAnimationPlaying()) return null;
        if (currentAction != null && currentAction == ability)
        {
            UnsetCurrentAction();
            return null;
        }
 

        UnsetCurrentAction();
        if (!ability.CanExecAction(true))
        {
            Debug.Log("Nope");
            return null;
        }
        ToastNotification.StopToast();

        currentAction = ability;
        currentAction.OnActionStart += OnActionUsed;
        currentAction.OnTarget += TargetedAction;
        currentAction.SelectAction();
       

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

    void OnActionUsed(UnitActionBase action)
    {
        AP_Used += action.EndTurnOnUse ? MaxAP : action.AP_Cost;

        CurrentTurnCost += (int) action.GetTimeCost();

        if (OnActionComplete != null) OnActionComplete(action);
      
        if (TurnSystem.HasTurn(Owner) && PanCamera.Instance != null) {
            PanCamera.Instance.PanToPos(Owner.currentTile.GetPosition());
        }

        UnsetCurrentAction();
        if (OnActionStarted != null) OnActionStarted(action);

        // Debug.Log(Owner.GetID() + " Action used" + action.ActionID);
       
    }

    void TargetedAction(UnitActionBase b, Transform tgt)
    {
       // Debug.Log("targeted action");
        if(OnTargetAction != null)
        {
            OnTargetAction(b, tgt);
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
