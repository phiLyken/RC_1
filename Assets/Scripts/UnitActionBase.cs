using UnityEngine;
using System.Collections;

public delegate void ActionEventHandler(UnitActionBase action);

public class UnitActionBase : MonoBehaviour {

    protected bool ActionInProgress;
    public ActionEventHandler OnExecuteAction;
    public StatInfo[] Requirements;
   

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
        UI_ActiveUnit.Instance.AbilityTF.text = "Ability: " + ActionID+"\n"+Descr;
    }

    protected virtual bool CanExecAction()
    {

        return HasRequirements() && !ActionInProgress;
    }

    public bool HasRequirements()
    {
        if(!Owner.HasAP(AP_Cost))
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
            Debug.Log("Coudlnt execute "+ActionID +" ap cost:"+AP_Cost+" / "+Owner.GetAPLeft()  );
        }
    }

     protected virtual void ActionExecuted()
    {
        Debug.Log(ActionID + " done");
        ActionInProgress = false;
        if (OnExecuteAction != null) OnExecuteAction(this);
    }

}
