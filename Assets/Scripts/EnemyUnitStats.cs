using UnityEngine;
using System.Collections;

public class EnemyUnitStats : UnitStats {


    public override void ReceiveDamage(UnitEffect_Damage dmg)
    {
        GetStat(StatType.HP).Amount -= dmg.GetDamage();

        if (GetStat(StatType.HP).Amount <= 0 && OnHPDepleted != null)
        {
            OnHPDepleted();
        }
        else { 
            Updated();
        }
    }
}
