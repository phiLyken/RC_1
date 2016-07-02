using UnityEngine;
using System.Collections;

public class UnitEffectContainer_Heal : UnitEffect_Container
{

    public Heal heal;

    public override UnitEffect GetEffect()
    {
        return heal;
    }

}
