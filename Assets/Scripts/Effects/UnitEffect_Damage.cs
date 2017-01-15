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

    public M_Math.R_Range DamageRange;

    int baked_damage = -1;

 

    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {
        UnitEffect_Damage _cc = base.MakeCopy(origin, host) as UnitEffect_Damage;
 
        _cc.baked_damage = UnityEngine.Random.Range(GetMin(), GetMax() + 1);


        return _cc;
    }


    int GetMin()
    {
        int value = 0;
        if(Instigator != null && UseAttackStat)
        {
            value = (int) (DamageRange.min + (Instigator as Unit).Stats.GetStatAmount(StatToUseMin));
               
        } else
        {
            value = (int) DamageRange.min;
        }

        return value * Mathf.RoundToInt( EffectBonus ); 
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
            value = (int) (DamageRange.max + (Instigator as Unit).Stats.GetStatAmount(StatToUseMax));

        }
        else
        {
            value = (int) DamageRange.max;
        }

        return value * Mathf.RoundToInt(EffectBonus);
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
        int max = GetMax()  ;
        string dmg_text = "";
        if(min == max)
        {
            dmg_text = max.ToString();
        } else
        {
            dmg_text = GetMin() + "-" + GetMax();
        }
        return dmg_text + " Damage";
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

    public override string ToString()
    {
        return base.ToString() + " \nBaked:" + baked_damage + " range:" + DamageRange.ToString();
    }
}