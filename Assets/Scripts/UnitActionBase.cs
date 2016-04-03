﻿using UnityEngine;
using System.Collections;

public delegate void ActionEventHandler(UnitActionBase action);

public class UnitActionBase : MonoBehaviour {

    protected bool ActionInProgress;
    public ActionEventHandler OnExecuteAction;
    public StatInfo[] Requirements;
    public StatInfo[] Cost;

    public int TurnTimeCost;

    public string Descr;

    [HideInInspector]
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
        ActionInProgress = false;
        if (OnExecuteAction != null) OnExecuteAction(this);
    }

}
