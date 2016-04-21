using UnityEngine;
using System.Collections;

public delegate void DamageEventHandler(Damage dmg);
[System.Serializable]
public class Damage
{
    public int amount = 20;
    public int base_damge = 0;
    public int bonus_damage = 0;
}
public interface IDamageable {

    void ReceiveDamage(Damage dmg);
   

}
