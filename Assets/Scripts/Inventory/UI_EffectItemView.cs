﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_EffectItemView : MonoBehaviour, IToolTip {
    public UnitEffect m_effect;

    public Image Icon;
    public Text Text;

    public UnitEffect GetEffect()
    {
        return m_effect;
    }
    public void SetEffect(UnitEffect new_effeect)
    {
        m_effect = new_effeect;
        Icon.sprite = new_effeect.Icon;
        Text.text = new_effeect.GetString();

    }

    public object GetItem()
    {
        return GetEffect();
    }
}