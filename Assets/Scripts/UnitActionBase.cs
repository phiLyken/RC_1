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
    
    public bool UsableInBaseCamp;
    public bool UseCharges;
    public bool EndTurnOnUse;

    public ItemTypes ChargeType;
   
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
        ResetCharge();
    }
     
    public void ResetCharge()
    {
        UnitInventory inv = Owner.GetComponent<UnitInventory>();

        if (inv != null && !inv.HasItem(ChargeType,0))
        {
            //  Debug.Log(LootBalance.GetBalance().LootConfigs.Count);

            IInventoryItem item_config = LootBalance.GetBalance().GetItem(ChargeType);

            if(item_config == null)
            {
                Debug.LogWarning("CAN FIND A CONSUMABLE FOR " + ChargeType.ToString());
                return;
            }
            inv.AddItem(item_config, ChargeMax);
        } 
       
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

  
    public bool HasRequirements(bool displayToast)
    {
        foreach (StatInfo s in Requirements)
        {
            if (Owner.Stats.GetStat(s.Stat).Amount < s.Amount)
            {
                Debug.Log("Not enough  " + UnitStats.StatToString(s.Stat));
                if (displayToast)
                {
                    ToastNotification.SetToastMessage2("Not enough " + UnitStats.StatToString(s.Stat));

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

    public bool HasCharges()
    {
        bool r = !UseCharges || GetChargesForType() > 0;
        if (!r) Debug.Log("No Charges");
        return r;
    }

    public int GetChargesForType()
    {
        ItemInInventory item = Owner.GetComponent<UnitInventory>().GetInventoryItem(ChargeType);
        if(item == null)
        {
            Debug.LogWarning("COULDNT FIND ITEM FOR TYPE " + ChargeType);
            return 0;
        }
        return item.count;
    }
    protected virtual void ActionCompleted()
    {
     
        
        ActionInProgress = false;
    }
     protected virtual void ActionExecuted()
    {
        Debug.Log(ActionID + " done");
      

        if (UseCharges)
        {
            UnitInventory inv = Owner.GetComponent<UnitInventory>();
            inv.ModifyItem(ChargeType, -1);           
        }

        if (OnExecuteAction != null) OnExecuteAction(this);
    }

   
    public virtual List<Tile> GetPreviewTiles()
    {
        return null;
    }


}
