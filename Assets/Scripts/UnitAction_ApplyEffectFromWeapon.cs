using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public enum WeaponMode { regular, int_attack }
public class UnitAction_ApplyEffectFromWeapon : UnitAction_ApplyEffect {
    
    public WeaponMode Mode;

    WeaponBehavior GetBehavior()
    {
        WeaponConfig wp = Owner.GetComponent<UnitInventory>().Weapon;
        return Mode == WeaponMode.regular ? wp.RegularBehavior : wp.IntAttackBehavior; 
    }

    protected override List<UnitEffect> GetEffects()
    {     

        WeaponBehavior Behavior = GetBehavior();

        List <UnitEffect> effects = Behavior.Effects.Select(container => container.GetEffect()).ToList();

        if(Behavior.IntBonus != null) {
           
            UnitEffect intBonus = Behavior.IntBonus.GetEffectForInstigator(   (int)Owner.Stats.GetStat(UnitStats.StatType.intensity).Amount);
           
            effects.AddRange(MyMath.GetListFromObject(intBonus));
        }

        return effects;
    }

    public override float GetRange()
    {
     return  Owner.GetComponent<UnitInventory>().Weapon.Range;
    }
}
