using UnityEngine;
using System.Collections;

public class UnitEffectContainer_Damage : UnitEffect_Container
{

    public Effect_Damage Damage;

    public override UnitEffect GetEffect()
    {
        return Damage;
    }

}
