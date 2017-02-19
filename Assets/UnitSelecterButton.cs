using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UnitSelecterButton : UI_ButtonGetSet<Unlockable<TieredUnit>> {

    public GameObject Selected;
    public GameObject InfoButtonTarget;

    UI_ButtonGetSet<ScriptableUnitConfig> _InfoButton;
    public UI_UnitTierLock_TierView UnitTiers;

    public UI_UnitMiniView Portrait;
    public Text ClassNameTF;   
    
    public void SetSelected()
    {
        Selected.SetActive(true);
    }

    public void SetUnselected()
    {
        Selected.SetActive(false);
    }

    public override void SetItem(Unlockable<TieredUnit> item, Action<Unlockable<TieredUnit>> button_Callback)
    {
       // Debug.Log("SetItem " + item.Item.Tiers[0].Config.ID);
        base.SetItem(item, button_Callback);
        ClassNameTF.text = item.Item.Tiers[0].Config.ID;
        Portrait.Init(item.Item.Tiers[0].Config, Color.white);

        item.AddUnlockListener(Updated);
        InfoButtonTarget.AddComponent<UI_ButtonGetSet_ScriptableUnitConfig>().SetItem(item.Item.Tiers[0].Config, SetUnitInfo);

        UnitTiers.Init(TieredUnit.Unlocks(item.Item.Tiers, PlayerLevel.Instance));
    }



    public override void Updated()
    {
        if(this == null || gameObject == null || !gameObject.activeSelf)
        {
            return;
        }
        TargetButton.interactable = m_Item.IsUnlocked();
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    protected override void OnSet(Unlockable<TieredUnit> item)
    {
        SetUnselected();
    }

    protected void SetUnitInfo(ScriptableUnitConfig info)
    {
        Debug.Log("SHOW INFO");
    }
}
