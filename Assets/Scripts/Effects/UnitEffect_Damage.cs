using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public delegate void DamageEventHandler(UnitEffect_Damage dmg);

[System.Serializable]
public class UnitEffect_Damage : UnitEffect
{
    public MyMath.R_Range DamageRange;

    int baked_damage = -1;

    public UnitEffect_Damage(UnitEffect_Damage origin) : base(origin)
    {
        DamageRange = origin.DamageRange;

        baked_damage = (int)origin.DamageRange.Value();

        Debug.Log("DMG  Baked " + baked_damage);
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

    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    protected override IEnumerator ApplyEffect(Unit target, UnitEffect effect)
    {
        //Make copy
        UnitEffect_Damage copy = new UnitEffect_Damage(effect as UnitEffect_Damage);

        TurnSystem.Instance.OnGlobalTurn += copy.OnGlobalTurn;

        if (copy.GetDamage() > 0)
        {
            if (target.GetComponent<Unit_EffectManager>().ApplyEffect(copy)) {
                copy.Effect_Host = target;
                copy.EffectTick();
            }

        }
        
        yield return null;
    }

    void EffectTick()
    {
        Ticked();
        Effect_Host.ReceiveDamage(this);
      
    }
    public override void SetPreview(UI_DmgPreview prev, Unit target)
    {

    }

    protected override void GlobalTurnTick()
    {
        EffectTick();
    }

}