using UnityEngine;
using System.Collections;
using System;

public class UnitEffectContainer_GainAdrenalinee : UnitEffect_Container {

    public UnitEffect_GainAdrenaline Effect;

    public override UnitEffect GetEffect()
    {
        return Effect;
    }
}
