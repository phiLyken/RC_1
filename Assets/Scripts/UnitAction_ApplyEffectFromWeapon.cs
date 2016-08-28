using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public enum WeaponMode { regular, int_attack }
public class UnitAction_ApplyEffectFromWeapon : UnitAction_ApplyEffect {
    
    public WeaponMode Mode;

    WeaponBehavior GetBehavior()
    {
        Weapon wp = Owner.GetComponent<UnitInventory>().EquipedWeapon;
        return Mode == WeaponMode.regular ? wp.RegularBehavior : wp.IntAttackBehavior; 
    }

    public override List<Tile> GetPreviewTiles()
    {
        return base.GetPreviewTiles(); 
    }    

    protected override List<UnitEffect> GetEffects()
    {
        List<UnitEffect> effects = GetRegularEffects();

        UnitEffect intBonus = GetIntBonus();

        if(intBonus != null)
        {
            effects.Add(intBonus );
        }

        return effects;
    }

    public UnitEffect GetIntBonus()
    {
        if (GetBehavior().IntBonus == null)
            return null;

        return GetBehavior().IntBonus.GetEffectForInstigator((int) Owner.Stats.GetStatAmount(StatType.adrenaline));
    }

    public List<UnitEffect> GetRegularEffects()
    {
        return GetBehavior().Effects;
    }

    public override float GetRange()
    {
        return Owner.GetComponent<UnitInventory>().EquipedWeapon.Range;
    }

    public override float GetTimeCost()
    {
        float base_cost =  base.GetTimeCost();

        int weaponDelay = GetBehavior().TimeDelay;

        return   (float)  Constants.GetAttackTimeDelay(base_cost, weaponDelay);
    }

    public override Sprite GetImage()
    {
        return GetBehavior().Icon;
    }
}
