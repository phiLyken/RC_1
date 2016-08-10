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

        WeaponBehavior Behavior = GetBehavior();

        List<UnitEffect> effects = Behavior.Effects;
        
        if(Behavior.IntBonus != null) {

            UnitEffect intBonus = Behavior.IntBonus.GetEffectForInstigator( (int) Owner.Stats.GetStatAmount(StatType.adrenaline));
           
            effects.AddRange(MyMath.GetListFromObject(intBonus));
        }

        return effects;
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
