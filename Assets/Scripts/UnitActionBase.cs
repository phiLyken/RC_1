using UnityEngine;
using System.Collections;



public class UnitActionBase : MonoBehaviour {

    public bool ActionInProgress;
    public ActionEventHandler OnExecuteAction;
    public StatInfo[] Requirements;

    public bool UsableInBaseCamp;

    public bool UseCharges;
    public int Charges;
    public int ChargeMax;

    public int TurnTimeCost;

    public string Descr;


    public int AP_Cost = 1;

    protected  Unit Owner;
    
    public string ActionID = "void";
    public void SetOwner(Unit o)
    {
        Owner = o;
    }

   
    public virtual void SelectAction()
    {
        string charges = "";
        if (UseCharges) charges = Charges.ToString();
        UI_ActiveUnit.Instance.AbilityTF.text = "Ability: " + ActionID+"("+charges+")\n"+Descr;
    }

    protected virtual bool CanExecAction()
    {

        return Owner.Actions.HasAP(AP_Cost) && HasRequirements() && !ActionInProgress && HasCharges() && !BlockedByCamp();
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
            if (Owner.Stats.GetStat(s.Stat).current < s.Amount)
            {
                Debug.Log("not enough  " + Owner.Stats.GetStat(s.Stat).ToString());
                return false;
            }
        }
        return true;
    }
    public virtual void UnSelectAction()
    {

    }
    public  void AttemptExection()
    {
        if (CanExecAction())
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
    protected void ActionCompleted()
    {
        Debug.Log("action completed ");
        Charges--;
        ActionInProgress = false;
    }
     protected virtual void ActionExecuted()
    {
        Debug.Log(ActionID + " done");
       
        if (OnExecuteAction != null) OnExecuteAction(this);
    }

}
