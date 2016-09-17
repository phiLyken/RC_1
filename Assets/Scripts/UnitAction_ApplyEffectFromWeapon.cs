using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


 
public class UnitAction_ApplyEffectFromWeapon : UnitAction_ApplyEffect {
    
    public int WeaponBehaviorIndex;

    WeaponBehavior GetBehavior()
    {
        Weapon wp = Owner.GetComponent<UnitInventory>().EquipedWeapon;
        if(WeaponBehaviorIndex > wp.Behaviors.Count)
        {
            Debug.LogError("SEMJON!!!! Weaponindex is higher than your mom");
            return null;
        }
        return wp.Behaviors[WeaponBehaviorIndex];
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
        return GetBehavior().TargetRule.GetRange(Owner);
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

    protected override StatInfo[] GetRequirements()
    {
        return GetBehavior().Requirements;
    }

    protected override TargetInfo GetTargetRules()
    {
        return GetBehavior().TargetRule;
    }
}
