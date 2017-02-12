﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UnitSelecterButton : UI_ButtonGetSet<Unlockable<TieredUnit>> {

    public GameObject Selected;
    public GameObject Locked;

    public GameObject InfoButtonTarget;

    UI_ButtonGetSet<ScriptableUnitConfig> _InfoButton;

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
        base.SetItem(item, button_Callback);
        ClassNameTF.text = item.Item.Tiers[0].Config.ID;
        Portrait.SetItem(item.Item.Tiers[0].Config);

        item.AddUnlockListener(Updated);
        InfoButtonTarget.AddComponent<UI_ButtonGetSet_ScriptableUnitConfig>().SetItem(item.Item.Tiers[0].Config, SetUnitInfo);
    }



    public override void Updated()
    {
        Locked.SetActive(!m_Item.IsUnlocked());
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