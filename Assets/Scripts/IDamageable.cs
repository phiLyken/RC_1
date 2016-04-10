using UnityEngine;
using System.Collections;

[System.Serializable]
public class Damage
{
    public int amount = 20;
}
public interface IDamageable {

    void ReceiveDamage(Damage dmg);
   

}
