using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void TargetListEvent(List<GameObject> targets);

public class UnitActionBase : MonoBehaviour {

    public UnitAnimationTypes ExecAnimation;
    public string TileViewState;

    [HideInInspector]
    public bool ActionInProgress;

    public TargetedAction OnTarget;
    public ActionEventHandler OnActionStart;
    public ActionEventHandler OnSelectAction;
    public ActionEventHandler OnUnselectAction;
    public ActionEventHandler OnActionComplete;

    public ActionTargetEventHandler OnTargetHover;
    public ActionTargetEventHandler OnTargetUnhover;

    public TargetListEvent OnTargetsFound;

    public StatInfo[] Requirements;

    public AbilityChargeController ChargeController;
    public AbilityTurnCostConfig TimeCost;
    
    public ActionSequence action_sequence;

    public bool UsableInBaseCamp;
    public bool EndTurnOnUse;

    public string Descr;
    
    public int AP_Cost = 1;

    protected Unit Owner;
    
    public Sprite Image;

    public virtual Sprite GetImage()
    {
        return Image;
    }

    public ActionButtonID target_button;

    public string ActionID = "void";

    public virtual void SetOwner(Unit o)
    {
        Owner = o;
        TimeCost.Init(o);
        ChargeController.Init(o);
        action_sequence =  GetComponent<ActionSequence>();

        if(action_sequence != null)
             action_sequence.Init(o);
    }

    public TargetHighLight TargetHighlightPrefab;

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
        if (!HasRequirements(displayToast)) {
            return false;
        }
        if (ActionInProgress) {
            if (displayToast) ToastNotification.SetToastMessage2("Other Action in Progress");
            return false;
        }
        if (!ChargeController.HasCharges())
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
       
        if (r) Debug.Log("^abilityCan not use in camp");
        return r;
    }

    public virtual StatInfo[] GetRequirements()
    {
        return Requirements;
    }
  
    public bool HasRequirements(bool displayToast)
    {
        foreach (StatInfo s in GetRequirements())
        {
            if (Owner.Stats.GetStatAmount(s.StatType) < s.Value)
            {
              if (displayToast)
                {
                        Debug.Log("^abilityNot enough  " + UnitStats.StatToString(s.StatType));
                        ToastNotification.SetToastMessage2("Not enough " + UnitStats.StatToString(s.StatType));
                }
                return false;
            }
        }
        return true;
    }

    public Unit GetOwner()
    {
        return Owner;
    }
    public virtual void UnSelectAction()
    {
        if (OnUnselectAction != null) OnUnselectAction(this);
    }

    public void AttemptAction(Component target)
    {       
        if (CanExecAction(true))
        {
            if(OnTarget != null)
            {
                OnTarget(this, target.transform);
            }
            ActionInProgress = true;
            ChargeController.UseCharge();

            ActionStarted(target);

            if (OnActionStart != null)
                OnActionStart(this);

           // Debug.Log("executing " + gameObject.name);
            
            if(action_sequence != null)
            { 
                action_sequence.StartSequence(GetExecAnimation(), Owner, target.transform, ()=> ActionExecuted(target))  ;
            } else
            {
                ActionExecuted(target);
            }

        } else
        {
           // Debug.Log("Coudlnt execute "+ActionID +" ap cost:"+AP_Cost+" / "+Owner.GetAPLeft()  );
        }
    }

    protected IEnumerator DelayedCompletion(float time)
    {
        yield return new WaitForSeconds(time);
        ActionCompleted();
    }

    protected void ActionCompleted()
    {
     
        ActionInProgress = false;

        if (OnActionComplete != null)
            OnActionComplete(this);
    }

    protected virtual void ActionStarted(Component target)
    {

       
    }

    protected virtual void ActionExecuted(Component target)
    {

    }
   
    public virtual List<Tile> GetPreviewTiles()
    {
        return null;
    }

    public virtual float GetTimeCost()
    {
        return TimeCost.GetCost();
    }
    public virtual TargetHighLight GetPreviewPrefab()
    {
        return TargetHighlightPrefab;
    }

    public virtual string GetTileViewState()
    {
        return TileViewState;
    }

    public virtual string GetActionID()
    {
        return ActionID;
    }

    public virtual UnitAnimationTypes GetExecAnimation()
    {
        return ExecAnimation;
    }

    public virtual bool GetEndsTurn()
    {
        return EndTurnOnUse;
    }

    public virtual string GetDescription()
    {
        return Descr;
    }
}