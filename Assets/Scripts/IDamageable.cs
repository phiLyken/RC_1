using UnityEngine;
using System.Collections;
using System;

public delegate void DamageEventHandler(Damage dmg);


public interface IUnitEffect
{
    void ApplyToUnit(Unit u);
    void SetPreview(UI_DmgPreview prev, Unit target);
}

[System.Serializable]
public class Damage : IUnitEffect
{
    [HideInInspector]
    public int amount = 0;

    public int GetDamage()
    {
        return UnityEngine.Random.Range(min, max);
    }
    [Tooltip("Amount of damage Dealt")]
    public int min = 20;

    [Tooltip("Amount of damage Dealt")]
    public int max = 20;

    [HideInInspector]
    public MyMath.R_Range bonus_range;

    [HideInInspector]
    public int base_damge = 0;
    [HideInInspector]
    public int bonus_damage = 0;

    public Damage(MyMath.R_Range dmg)
    {
        min = (int) dmg.min;
        max = (int) dmg.max;
    }

    public Damage()
    {
        min = 100;
        max = 200;
        amount = GetDamage();
    }

    public void ApplyToUnit(Unit target)
    {
        target.ReceiveDamage(this);
    }

    public void SetPreview(UI_DmgPreview prev, Unit target)
    {
       prev.MainTF.text = min + "-" + max;

       bool showBonus = bonus_range.max > 0;

       prev.Icon.gameObject.SetActive(showBonus);
       prev.IconTF.gameObject.SetActive(showBonus);
        if (showBonus)
        {
          prev.IconTF.text = "+" + bonus_range.min + "-" + (bonus_range.max - 1);

        }
    }

}

public class Heal : IUnitEffect
{
    
    public void ApplyToUnit(Unit target)
    {
        (target.Stats as PlayerUnitStats).Rest();   
    }
    public void SetPreview(UI_DmgPreview prev, Unit target)
    {
        prev.Icon.gameObject.SetActive(false);
        prev.MainTF.text = "RESTORE O²";

        prev.IconTF.text = ((target.Stats as PlayerUnitStats).Max - (target.Stats as PlayerUnitStats).Will).ToString(); 
    }

} 

public interface IDamageable {

    void ReceiveDamage(Damage dmg);  

}
