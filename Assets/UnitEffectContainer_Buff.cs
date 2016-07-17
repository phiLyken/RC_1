using UnityEngine;
using System.Collections;

public class UnitEffectContainer_Buff : UnitEffect_Container {

    public string BuffText;

    public UnitEffect_BuffStats buff;

    public override UnitEffect GetEffect()
    {
        return buff;
    }
}
