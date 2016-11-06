﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_EffectItemView : MonoBehaviour, IToolTip {
    public UnitEffect m_effect;

    public Sprite Target_Owner;
    public Sprite Target_Target;

    public Image Icon;
    public Image BG;

    public Text Text;

    public UnitEffect GetEffect()
    {
        return m_effect;
    }
    public void SetEffect(UnitEffect new_effect)
    {

        UI_ActionBar_Button_ColorSetting setting = UI_ActionBar_Button_ColorSetting.GetInstance();

        BG.sprite = new_effect.TargetMode == UnitEffect.TargetModes.Owner ? Target_Owner : Target_Target;

        m_effect = new_effect;
       
       
        if(new_effect.Icon != null)
        {
            Icon.sprite = new_effect.Icon;

            Icon.color = new_effect.EffectBonus > 1 ? setting.BTN_ADR_Rush.Icon : setting.BTN_Active.Icon;
        }


        Text.text = new_effect.GetShortHandle();
        Text.color = new_effect.EffectBonus > 1 ? setting.BTN_ADR_Rush.Icon : setting.BTN_Active.Icon;

    }

    public object GetItem()
    {
        return GetEffect();
    }
}
