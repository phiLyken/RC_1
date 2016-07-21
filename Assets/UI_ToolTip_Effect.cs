using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ToolTip_Effect : UI_ToolTip_Base {
    
    public Text Description;

    public void SetEffect(UnitEffect effect)
    {
        Description.text = "Some information about " + effect.GetType().ToString();
    }

    public override void SetItem(object obj)
    {
        SetEffect((UnitEffect)obj);
    }
}
