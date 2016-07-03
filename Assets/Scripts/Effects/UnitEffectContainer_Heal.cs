using UnityEngine;
using System.Collections;

public class UnitEffectContainer_Heal : UnitEffect_Container
{

    public UnitEffect_Heal heal;

    public override UnitEffect GetEffect()
    {
        return heal;
    }

}
