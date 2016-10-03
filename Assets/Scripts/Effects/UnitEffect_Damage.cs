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

        _cc.baked_damage = UnityEngine.Random.Range(GetMin(), GetMax());
        _cc.isCopy = true;
        return _cc;
    } 

    int GetMin()
    {
        int value = 0;
        if(Instigator != null && UseAttackStat)
        {
            value = (int) (DamageRange.min + ((Unit) Instigator).Stats.GetStatAmount(StatToUseMin));
               
        } else
        {
            value = (int) DamageRange.min;
        }

        return value * EffectBonus; 
    }

    public UnitEffect_Damage(int dmg)
    {
        baked_damage = dmg;
    }

    int GetMax()
    {
        int value = 0;
        if (Instigator != null && UseAttackStat)
        {
            value = (int) (DamageRange.max + ((Unit) Instigator).Stats.GetStatAmount(StatToUseMax));

        }
        else
        {
            value = (int) DamageRange.max;
        }

        return value * EffectBonus;
    }
    public int GetDamage()
    {
        if (!isCopy)
        {
            Debug.LogWarning("DMG NOT BAKED");
        }
        return baked_damage;
    }
    

    public override string GetToolTipText()
    {
        return GetShortHandle() ;
    }

    public override string GetShortHandle()
    {
        if (isCopy)
            return baked_damage + " DAMAGE";

        int min = GetMin();
        int max = GetMax();
        string dmg_text = "";
        if(min == max)
        {
            dmg_text = max.ToString();
        } else
        {
            dmg_text = GetMin() + "-" + GetMax();
        }
        return dmg_text + " Damage" + EffectBonus;
        ;
    }
    protected override void EffectTick()
    {
        Ticked();
        Effect_Host.ReceiveDamage(this);      
    }

    protected override void GlobalTurnTick()
    {
        EffectTick();
    }

    public override string GetNotificationText()
    {
        return baked_damage + " DAMAGE";
    }
}