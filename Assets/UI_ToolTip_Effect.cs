using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ToolTip_Effect : UI_ToolTip_Base {

    public Image DurationIcon;
    public Text DurationTF;
        
    public Text Description;

    public void SetEffect(UnitEffect effect)
    {
        Description.text = effect.GetToolTipText();

        if(effect.EffectBonus > 1){
            Description.color = UI_AdrenalineRush.ADR_Color;
        }

        if(effect.GetMaxDuration()> 0)
        {
            DurationTF.text = effect.GetDurationLeft().ToString();
            DurationIcon.gameObject.SetActive(true);
        } else
        {
            //DurationIcon.gameObject.SetActive(false);
            DurationTF.text = "instant";
        }
    }

    public override void SetItem(object obj)
    {
        SetEffect((UnitEffect)obj);
    }
}
