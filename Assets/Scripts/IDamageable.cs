using UnityEngine;
using System.Collections;

public delegate void DamageEventHandler(Damage dmg);
[System.Serializable]
public class Damage
{
    [HideInInspector]
    public int amount = 0;

    public int GetDamage()
    {
        return Random.Range(min, max);
    }
    [Tooltip("Amount of damage Dealt")]
    public int min = 20;

    [Tooltip("Amount of damage Dealt")]
    public int max = 20;

   
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
    }
}
public interface IDamageable {

    void ReceiveDamage(Damage dmg);
   

}
