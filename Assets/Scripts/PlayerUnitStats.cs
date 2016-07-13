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
        AddInt(int_received, false);

        if( GetStatAmount(StatType.will) <= 0 && OnHPDepleted != null)
        {
            OnHPDepleted();
        }

    }
    public void AddInt(int amount, bool consumeWill)
    {

        int Max = GetStatAmount(StatType.max);
        int Int = GetStatAmount(StatType.intensity);
        int Will = GetStatAmount(StatType.will);

        if (consumeWill)
        {
            //if we will is consumed, we make sure to not consume the last will
            amount = Mathf.Min(amount, Max - (1 + Int));
        }
        else  
        {
            //if adding int does not consume will (e.g. when receiving damage), the incoming int is truncated to not exceed the cap
            amount = Mathf.Min(amount, Max - (Will + Int));
        }

        SetStatAmount(StatType.intensity, Mathf.Min(Mathf.Max(Int + amount, 0), Max));

        //in case int has been increased  more than the cap, "will" will be reduced
        SetStatAmount(StatType.will, Mathf.Min(Will, Max - Int));

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
