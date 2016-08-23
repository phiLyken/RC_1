using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public delegate void DamageEventHandler(UnitEffect_Damage dmg);

[System.Serializable]
public class UnitEffect_Damage : UnitEffect
{
    public bool UseAttackStat;
    public StatType StatToUseMin;
    public StatType StatToUseMax;

    public MyMath.R_Range DamageRange;

    int baked_damage = -1;

    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override UnitEffect MakeCopy(UnitEffect original, Unit host)
    {
        UnitEffect_Damage _cc = (UnitEffect_Damage)( (UnitEffect_Damage) original).MemberwiseClone() ;

        MyMath.R_Range range = _cc.DamageRange;

        if (UseAttackStat)
        {
            range.min += ((Unit) Instigator).Stats.GetStatAmount(StatToUseMin);
            range.max += ((Unit) Instigator).Stats.GetStatAmount(StatToUseMax);
        }
        _cc.baked_damage = (int) range.Value();

        return _cc;
    }

    public int GetDamage()
    {
        if (baked_damage < 0)
        {
            Debug.LogWarning("DMG NOT BAKED");
        }
        return baked_damage;
    }
    public UnitEffect_Damage()
    {
        baked_damage = 5;
    }

    public override string GetString()
    {
        return GetDamage() + " DAMAGE";
    }

    protected override void  EffectTick()
    {
        Ticked();
        Effect_Host.ReceiveDamage(this);      
    }

    protected override void GlobalTurnTick()
    {
        EffectTick();
    }

}