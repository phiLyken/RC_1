using UnityEngine;
using System.Collections;



public class UnitActionBase : MonoBehaviour {

    public bool ActionInProgress;
    public ActionEventHandler OnExecuteAction;
    public StatInfo[] Requirements;

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

        return HasRequirements() && !ActionInProgress;
    }

    void Start()
    {
        if (UseCharges) Charges = ChargeMax;
    }
  
    public bool HasRequirements()
    {
        if(!Owner.Actions.HasAP(AP_Cost))
        {
            Debug.Log("not enough ap");
            return false;
        }

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
        return !UseCharges || Charges > 0;
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
