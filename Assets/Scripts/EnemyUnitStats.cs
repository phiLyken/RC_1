using UnityEngine;
using System.Collections;

public class EnemyUnitStats : UnitStats {


    public override void ReceiveDamage(Damage dmg)
    {
        GetStat(StatType.HP).Amount -= dmg.amount;

        if (GetStat(StatType.HP).Amount <= 0 && OnHPDepleted != null)
        {
            OnHPDepleted();
        }
        else { 
            Updated();
        }
    }
}
