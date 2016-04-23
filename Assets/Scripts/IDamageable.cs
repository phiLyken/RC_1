using UnityEngine;
using System.Collections;

public delegate void DamageEventHandler(Damage dmg);
[System.Serializable]
public class Damage
{
    [Tooltip("Amount of damage Dealt")]
    public int amount = 20;

    [HideInInspector]
    public int base_damge = 0;
    [HideInInspector]
    public int bonus_damage = 0;
}
public interface IDamageable {

    void ReceiveDamage(Damage dmg);
   

}
