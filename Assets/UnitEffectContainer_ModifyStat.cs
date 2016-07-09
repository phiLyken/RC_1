using UnityEngine;
using System.Collections;

public class UnitEffectContainer_ModifyStat : UnitEffect_Container {

    public UnitEffect_ModifyStat Effect;

    public override UnitEffect GetEffect()
    {
        return Effect;
    }
}
