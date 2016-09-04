﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


[System.Serializable]
public class UnitEffect_Heal : UnitEffect
{
    public override UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {
        UnitEffect_Heal heal = (UnitEffect_Heal)origin;
        return (UnitEffect_Heal)heal.MemberwiseClone();
    }

    protected override void EffectTick()
    {
        Debug.Log("heal effect..");
        (Effect_Host.Stats).Rest();
        Ticked();
    }

    public override string GetToolTipText()
    {
        return "RESTED";
    }
}
