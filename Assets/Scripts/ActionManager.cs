using UnityEngine;
using System.Collections;
using System.Linq;

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
            action.Charges = action.ChargeMax;
        }
    }
    public static ActionManagerEventHander OnActionManagerActivated;
    
    public int GetOwnerID()
    {
        return Owner.OwnerID;
    }
   
    public UnitActionBase[] Actions;
    UnitActionBase currentAction;

    public ActionEventHandler OnActionSelected;
    public ActionEventHandler OnActionUnselected;
    public ActionEventHandler OnActionComplete;

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
		Actions = raw_actions.OrderBy(o => o.orderID).ThenBy(o => o.ActionID).ToArray();

	}
    public void SetOwner(Unit owner)
    {
        Owner = owner;
        Owner.OnTurnEnded += unit =>
        {
            Reset();
        };

        Owner.OnTurnStart += unit =>
        {
            AP_Used = 0;
            if (OnActionManagerActivated != null) OnActionManagerActivated(this);        
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

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SelectAbility(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectAbility(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SelectAbility(2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SelectAbility(3);
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SelectAbility(4);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SelectAbility(5);
        }
    }

    public UnitActionBase SelectAbility(int index)
    {
        if (Actions.Length <= index)
        {
            Debug.LogWarning("No ability #" + index);
            return null;
        }
        return  SelectAbility(Actions[index]);
    }
    public UnitActionBase SelectAbility(UnitActionBase ability)
    {
        //  Debug.Log("Select Ability " + index);
        //  if (index > Actions.Length) return;
        if (Unit.SelectedUnit != Owner || !HasAP()) return null;
        if (currentAction != null && currentAction == ability)
        {
            UnsetCurrentAction();
            return null;
        }

        UnsetCurrentAction();
        if (!ability.CanExecAction(true))
        {
            return null;
        }
        ToastNotification.StopToast();

        currentAction = ability;
        currentAction.OnExecuteAction += OnActionUsed;
        currentAction.SelectAction();

        if (OnActionSelected != null) OnActionSelected(currentAction);
        return currentAction;

    }

    private void UnsetCurrentAction()
    {
        if (currentAction == null) return;

        currentAction.UnSelectAction();
        currentAction.OnExecuteAction -= OnActionUsed;       
        currentAction = null;    

        if (OnActionUnselected != null) OnActionUnselected(currentAction);
       // if (OnActionManagerActivated != null) OnActionManagerActivated(this);

    }

    void OnActionUsed(UnitActionBase action)
    {
        AP_Used += action.EndTurnOnUse ? MaxAP : action.AP_Cost;

        CurrentTurnCost += action.TurnTimeCost;

        if (OnActionComplete != null) OnActionComplete(action);

      
        if (TurnSystem.HasTurn(Owner) && PanCamera.Instance != null) {
            PanCamera.Instance.PanToPos(Owner.currentTile.GetPosition());
        }

        UnsetCurrentAction();
        // Debug.Log(Owner.GetID() + " Action used" + action.ActionID);
       
    }

	string GetActionInfos(UnitActionBase[] actions)
    {
        string s = "";
		for (int i = 0; i < actions.Length; i++)
        {
            string charges = "";
			if (actions[i].UseCharges) charges = "   ["+actions[i].Charges.ToString()+"]";

			s += (i + 1).ToString() + ": " + actions[i].ActionID + charges+"\n";
        }
        return s;
    }
    public T GetAcionOfType <T>()
    {
        return Actions.OfType<T>().ToList()[0];
    }
    public UnitActionBase GetAction(string id)
    {
        foreach (var action in Actions) if (action.ActionID == id) return action;

        Debug.LogWarning("Could not find action " + id);
        return null;
    }
}
