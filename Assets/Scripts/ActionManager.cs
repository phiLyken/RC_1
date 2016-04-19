using UnityEngine;
using System.Collections;

public delegate void ActionEventHandler(UnitActionBase action);

public class ActionManager : MonoBehaviour {

    Unit Owner;

    public int AP_Used = 99;
    public  int MaxAP = 2;
    public int CurrentTurnCost = 0;
    
    public void RestCharges()
    {
        foreach(UnitActionBase action in Actions)
        {
            action.Charges = action.ChargeMax;
        }
    }   

    public UnitActionBase[] Actions;
    UnitActionBase currentAction;

    public ActionEventHandler OnActionSelected;
    public ActionEventHandler OnActionUnselected;

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

    void Awake()
    {
        Owner = gameObject.GetComponent<Unit>();
        Owner.OnTurnEnded += unit =>
        {
            Reset();
        };

        Owner.OnTurnStart += unit =>
        {
            AP_Used = 0;
        };

        Actions = GetComponentsInChildren<UnitActionBase>();
        foreach (UnitActionBase action in Actions) action.SetOwner(Owner);
    }

    public int GetAPLeft()
    {
       // Debug.Log((MaxAP - AP_Used).ToString());
        return (MaxAP - AP_Used);
    }

    public void Reset()
    {
        Debug.Log("Resetting action manager");
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
            Debug.Log("not enough AP");
        }
        return r;
    }

    public void SkipTurn()
    {
        Debug.Log("skip");
        AP_Used = MaxAP;
        CurrentTurnCost = 15;
    }
    void Update()
    {

       
        if (!TurnSystem.HasTurn(Owner as ITurn)) return;
      //  Debug.Log(Owner.GetID() + "has  turn");
        if (Owner.OwnerID != 0) return;
     
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SkipTurn();
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
        
        currentAction = ability;
        currentAction.OnExecuteAction += OnActionUsed;
        currentAction.SelectAction();
        if (OnActionSelected != null) OnActionSelected(currentAction);
        return currentAction;

    }

    private void UnsetCurrentAction()
    {
        if (currentAction == null) return;

        UI_ActiveUnit.Instance.AbilityTF.text = GetActionInfos();
        currentAction.UnSelectAction();
        currentAction.OnExecuteAction -= OnActionUsed;       
        currentAction = null;

        if (OnActionUnselected != null) OnActionUnselected(currentAction);

    }

    void OnActionUsed(UnitActionBase action)
    {
        AP_Used += action.AP_Cost;
        CurrentTurnCost += action.TurnTimeCost;
        // Debug.Log(TurnTime);
        if (TurnSystem.HasTurn(Owner))
            PanCamera.Instance.PanToPos(Owner.currentTile.GetPosition());
        UnsetCurrentAction();
        Debug.Log(Owner.GetID() + " Action used" + action.ActionID);
    }
    string GetActionInfos()
    {
        string s = "";
        for (int i = 0; i < Actions.Length; i++)
        {
            string charges = "";
            if (Actions[i].UseCharges) charges = Actions[i].Charges.ToString();
            s += (i + 1).ToString() + ":" + Actions[i].ActionID + "["+charges+"]\n";
        }
        return s;
    }

    public UnitActionBase GetAction(string id)
    {
        foreach (var action in Actions) if (action.ActionID == id) return action;

        Debug.LogWarning("Could not find action " + id);
        return null;
    }
}
