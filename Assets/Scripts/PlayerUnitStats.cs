using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerUnitStats : UnitStats    
{

    protected override void UpdatedBuffs()
    {
        MaxUpdated();
    }

    public override void ReceiveDamage(UnitEffect_Damage dmg)
    {
        int dmg_received = (-(dmg.GetDamage()));
        int int_received = (int)(Mathf.Abs(dmg_received) * Constants.RCV_DMG_TO_INT);

        Debug.Log(this.name + " rcv damge " + dmg_received + "  rcvd multiplier:" + "WTF" + "  +int=" + int_received);

        AddWill(dmg_received);

        int x, y = 0;
        AddInt(int_received, false,out x, out y);

        if( GetStatAmount(StatType.will) <= 0 && OnHPDepleted != null)
        {
            OnHPDepleted();
        }

    }
    public void AddInt(int amount, bool consumeWill, out int removed_will, out int added_int)
    {
        removed_will = 0;
        added_int = 0;

        int Max = GetStatAmount(StatType.max);
        int Int = GetStatAmount(StatType.intensity);
        int Will = GetStatAmount(StatType.will);

        int Combined = Will + Int;
        int Free = Max - Combined;

        if (consumeWill)
        {
           
            amount = Mathf.Min(amount, Max - (1 + Int));
        }
        else  
        {
           
            amount = Mathf.Min(amount, Free);
        }

        int _new_int = Mathf.Min(Mathf.Max(Int + amount, 0), Max);

        added_int = _new_int - Int;

        SetStatAmount(StatType.intensity, _new_int);

        //in case int has been increased  more than the cap, "will" will be reduced

        int _new_will = Mathf.Min(Will, Max - _new_int);
        removed_will = Will - _new_will;
        SetStatAmount(StatType.will, _new_will);

    }

    //recalc INT / WILL based on max
    public void MaxUpdated()
    {
        int Max = GetStatAmount(StatType.max);
        int Int = GetStatAmount(StatType.intensity);
        int Will = GetStatAmount(StatType.will);

        SetStatAmount(StatType.intensity, Int -   Mathf.Max( (Int + Will) - Max, 0));

    }
    public void Rest()
    {
        Debug.Log(" rest");
        SetStatAmount(StatType.intensity, 0);
        SetStatAmount(StatType.will, GetStatAmount(StatType.max));
    }
    public void AddWill(int amount)
    {
        int Max = GetStatAmount(StatType.max);
        int Int = GetStatAmount(StatType.intensity);
        int Will = GetStatAmount(StatType.will);
        //will is capped by the max-current int
        //e.g. a unit can have 5 resources but has 3 int, then will can never be larger than 3
        SetStatAmount(StatType.will, Mathf.Max(Mathf.Min(Will + amount, Max - Int), 0));
    }
    

}
