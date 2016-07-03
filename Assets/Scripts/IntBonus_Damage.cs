using UnityEngine;
public class IntBonus_Damage : IntBonus
{
    public float int_to_damage_min;
    public float int_to_damage_max;

    public override UnitEffect GetEffectForInstigator(int _int)
    {
        //copies the effect
        UnitEffect_Damage dmg = new UnitEffect_Damage(Effect.GetEffect() as UnitEffect_Damage);

        //modifies the ranges
        dmg.DamageRange = new MyMath.R_Range(dmg.DamageRange.min + int_to_damage_min * _int, dmg.DamageRange.max + int_to_damage_max * _int);

        Debug.Log("INTBONUS  " + dmg.DamageRange.min+ " - "+ dmg.DamageRange.max);
        return dmg;
    }
}


