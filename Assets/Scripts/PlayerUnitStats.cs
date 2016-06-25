using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerUnitStats : UnitStats
{    public int Max
    {
        get { return (int)GetStat(StatType.max).Amount; }
        set
        {
            GetStat(StatType.max).Amount = value;
            Updated();
        }
    }
    public int Will
    {
        get { return (int)GetStat(StatType.will).Amount; }
        set
        {
            GetStat(StatType.will).Amount = value;
            Updated();
        }
    }
    public int Int
    {
        get { return (int)GetStat(StatType.intensity).Amount; }
        set
        {
            GetStat(StatType.intensity).Amount = value;
            Updated();
        }
    }

    public override void ReceiveDamage(Damage dmg)
    {
        int dmg_received = (-(dmg.amount));
        int int_received = (int)(Mathf.Abs(dmg_received) * Constants.RCV_DMG_TO_INT);

        Debug.Log(this.name + " rcv damge " + dmg_received + "  rcvd multiplier:" + "WTF" + "  +int=" + int_received);

        AddWill(dmg_received);
        AddInt(int_received, false);

        if(Will <= 0 && OnHPDepleted != null)
        {
            OnHPDepleted();
        }

    }
    public void AddInt(int amount, bool consumeWill)
    {
        if (consumeWill)
        {
            //if we will is consumed, we make sure to not consume the last will
            amount = Mathf.Min(amount, Max - (1 + Int));
        }
        else if
      (!consumeWill)
        {
            //if adding int does not consume will (e.g. when receiving damage), the incoming int is truncated to not exceed the cap
            amount = Mathf.Min(amount, Max - (Will + Int));
        }

        Int = Mathf.Min(Mathf.Max(Int + amount, 0), Max);

        //in case int has been increased  more than the cap, "will" will be reduced
        Will = Mathf.Min(Will, Max - Int);


    }

    public void Rest()
    {
        Int = 0;
        Will = Max;
    }
    public void AddWill(int amount)
    {
        //will is capped by the max-current int
        //e.g. a unit can have 5 resources but has 3 int, then will can never be larger than 3
        Will = Mathf.Max(Mathf.Min(Will + amount, Max - Int), 0);
    }




}
