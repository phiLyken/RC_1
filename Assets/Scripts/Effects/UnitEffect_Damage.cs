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
        UnitEffect_Damage eff = new UnitEffect_Damage(effect as UnitEffect_Damage);

        if (eff.GetDamage() > 0 && !target.IsDead())
        {
            EffectNotification.SpawnDamageNotification(target.transform, eff);

            target.ReceiveDamage(eff);

        }
        
        yield return null;
    }

    public override void SetPreview(UI_DmgPreview prev, Unit target)
    {

    }

}