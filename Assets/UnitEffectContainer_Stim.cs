using UnityEngine;
using System.Collections;
using System;

public class UnitEffectContainer_Stim : UnitEffect_Container {

    public UnitEffect_stim Stim;
    public override UnitEffect GetEffect()
    {
        return Stim;
    }
}
