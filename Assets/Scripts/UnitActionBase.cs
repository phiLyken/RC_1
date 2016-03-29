using UnityEngine;
using System.Collections;

public delegate void ActionEventHandler(UnitActionBase action);

public class UnitActionBase : MonoBehaviour {

    public ActionEventHandler OnExecuteAction;
    public StatInfo[] Requirements;
    public StatInfo[] Cost;

    public int AP_Cost;

    protected  Unit Owner;

    public void SetOwner(Unit o)
    {
        Owner = o;
    }

    public virtual void SelectAction()
    {

    }

    protected virtual bool CanExecAction()
    {

        return HasRequirements();
    }

    public bool HasRequirements()
    {
        if(!Owner.HasAP(AP_Cost))
        {
            return false;
        }
        foreach (StatInfo s in Requirements)
        {
            if (Owner.Stats.GetStat(s.Stat).current < s.Amount) return false;
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
            ApplyCost();
            ActionExecuted();
            if (OnExecuteAction != null) OnExecuteAction(this);          
        }
    }

    void ApplyCost()
    {
        foreach (StatInfo s in Cost)
        {
            Owner.Stats.GetStat(s.Stat).ModifyStat(s.Amount);
            Owner.UpdateUI();
        }
    }
     protected virtual void ActionExecuted()
    {
        
    }

}
