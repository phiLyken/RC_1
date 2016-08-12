using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void TargetListEvent(List<GameObject> targets);

public class UnitActionBase : MonoBehaviour {
    [HideInInspector]
    public bool ActionInProgress;

    public ActionEventHandler OnExecuteAction;
    public ActionEventHandler OnSelectAction;
    public ActionEventHandler OnUnselectAction;

    public ActionTargetEventHandler OnTargetHover;
    public ActionTargetEventHandler OnTargetUnhover;

    public TargetListEvent OnTargetsFound;

    public StatInfo[] Requirements;

    public AbilityChargeController ChargeController;
    public AbilityTurnCostConfig TimeCost;

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

    public UnitAnimationTypes Animation;

    [HideInInspector]
    public int orderID;

    public string ActionID = "void";

    public void SetOwner(Unit o)
    {
        Owner = o;
        TimeCost.Init(o);
        ChargeController.Init(o);
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
       
        if (r) Debug.Log("Can not use in camp");
        return r;
    }

  
    public bool HasRequirements(bool displayToast)
    {
        foreach (StatInfo s in Requirements)
        {
            if (Owner.Stats.GetStatAmount(s.StatType) < s.Value)
            {
              if (displayToast)
                {
                        Debug.Log("Not enough  " + UnitStats.StatToString(s.StatType));
                        ToastNotification.SetToastMessage2("Not enough " + UnitStats.StatToString(s.StatType));
                }
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


    protected virtual void ActionCompleted()
    {
        ActionInProgress = false;
    }

     protected virtual void ActionExecuted()
    {


        if (OnExecuteAction != null) OnExecuteAction(this);
    }

   
    public virtual List<Tile> GetPreviewTiles()
    {
        return null;
    }

    public virtual float GetTimeCost()
    {
        return TimeCost.GetCost();
    }
    

}


