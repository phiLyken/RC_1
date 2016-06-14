using UnityEngine;
using System.Collections;



public class UnitActionBase : MonoBehaviour {
    [HideInInspector]
    public bool ActionInProgress;
    public ActionEventHandler OnExecuteAction;
    public ActionEventHandler OnSelectAction;
    public ActionEventHandler OnUnselectAction;
    public StatInfo[] Requirements;
    
    public bool UsableInBaseCamp;
    public bool UseCharges;
    public bool EndTurnOnUse;

    public int Charges;
    public int ChargeMax;

    public int TurnTimeCost;

    public string Descr;
    
    public int AP_Cost = 1;

    protected  Unit Owner;

    public Sprite Image;
    /// <summary>
    /// used for ordering the abilities in lists
    /// </summary>
    [HideInInspector]
    public int orderID;

    public string ActionID = "void";
    public void SetOwner(Unit o)
    {
        Owner = o;
    }
       
    public virtual void SelectAction()
    {
        if (OnSelectAction != null) OnSelectAction(this);
    }

    public virtual bool CanExecAction(bool displayToast)
    {

        if (!Owner.Actions.HasAP(AP_Cost)) {
            if(displayToast)  ToastNotification.SetToastMessage2("Not enough AP");
            return false;
        }
        if (!HasRequirements()) {
            return false;
        }
        if (ActionInProgress) {
            if (displayToast) ToastNotification.SetToastMessage2("Other Action in Progress");
            return false;
        }
        if (!HasCharges())
        {
            if (displayToast) ToastNotification.SetToastMessage2("Not Enough Charges");
            return false;
        }

        if (BlockedByCamp())
        {
            if (displayToast) ToastNotification.SetToastMessage2("Can not use in Camp");
            return false;
        }

        return true;

    }
    bool BlockedByCamp()
    {
        bool r = !UsableInBaseCamp && Owner.currentTile.isCamp;
       
        if (r) Debug.Log("Can not use in camp");
        return r;
    }
    void Start()
    {
        if (UseCharges) Charges = ChargeMax;
    }
  
    public bool HasRequirements()
    {
        foreach (StatInfo s in Requirements)
        {
            if (Owner.Stats.GetStat(s.Stat).Amount < s.Amount)
            {
                Debug.Log("Not enough  " + s.ToString());
                ToastNotification.SetToastMessage2("Not enough " + s.Stat.ToString());
                return false;
            }
        }
        return true;
    }
    public virtual void UnSelectAction()
    {
        if (OnUnselectAction != null) OnUnselectAction(this);
    }

    public void AttemptExection()
    {
        if (CanExecAction(true))
        { 
            ActionExecuted();              
        } else
        {
           // Debug.Log("Coudlnt execute "+ActionID +" ap cost:"+AP_Cost+" / "+Owner.GetAPLeft()  );
        }
    }

    public bool HasCharges()
    {
        bool r = !UseCharges || Charges > 0;
        if (!r) Debug.Log("No Charges");
        return r;
    }
    protected virtual void ActionCompleted()
    {
      //  Debug.Log("action completed ");
        Charges--;
        ActionInProgress = false;
    }
     protected virtual void ActionExecuted()
    {
       // Debug.Log(ActionID + " done");

        if (OnExecuteAction != null) OnExecuteAction(this);
    }

}
