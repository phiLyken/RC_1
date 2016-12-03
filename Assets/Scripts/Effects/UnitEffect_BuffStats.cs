﻿using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class UnitEffect_BuffStats : UnitEffect {

    public StatBuff  Buff ;
    
    public override string GetToolTipText()
    {
        return   GetShortHandle();
    }

    public override string GetShortHandle()
    {
        return GetModifier().ToString("+#;-#;0") + " " + UnitStats.StatToString(Buff.Type) ;
       
    }

    public override string GetNotificationText()
    {
        return GetShortHandle();
    }

    float GetModifier()
    {
        return Buff.Modifier * EffectBonus;
    }

}

[System.Serializable]
public class StatBuff
{
    public StatType Type;
    public float Modifier;
    
}
