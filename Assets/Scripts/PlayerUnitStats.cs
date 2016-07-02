using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerUnitStats : UnitStats    
{
    /*
        max and move range return buffed stats as those can be influenced by the inventory
        int and will are stats but they only have a current
        the SETTER of max and move sets the value in the associated statinfos 
        but the GETTER of max and move gets local statinfos + buffs
        So max and move will not be the same when / getting setting
    */
    UnitInventory inventory;

    float GetBuffedStat(StatType type, UnitInventory inventory)
    {
        float amount = 0;
        if(inventory != null) { 

            List<StatInfo> buffs = inventory.GetAllBuffs();
            if (buffs.Count > 0)
            {
                buffs = buffs.Where(st => st.Stat == type).ToList();
                amount += buffs.Sum(st => st.Amount);
             }
        }
        amount += (int)GetStat(type).Amount;
        return amount;
    }
    public int Max
    {
        get {
            return (int)GetBuffedStat(StatType.max, inventory);
        }
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

    public float  MoveRange
    {
        get { return GetBuffedStat(StatType.move_range, inventory); ; }
        set
        {
            GetStat(StatType.move_range).Amount = value;
            Updated();
        }
    }

    
    public override void ReceiveDamage(Effect_Damage dmg)
    {
        int dmg_received = (-(dmg.GetDamage()));
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
        else  
        {
            //if adding int does not consume will (e.g. when receiving damage), the incoming int is truncated to not exceed the cap
            amount = Mathf.Min(amount, Max - (Will + Int));
        }

        Int = Mathf.Min(Mathf.Max(Int + amount, 0), Max);

        //in case int has been increased  more than the cap, "will" will be reduced
        Will = Mathf.Min(Will, Max - Int);



        //  Debug.Log(" int amount added " + amount + "  consume: " + consumeWill);
        /*
        int truncated = amount;
        if (!consumeWill)
        {
            truncated = Mathf.Min(amount, Max - (Will + Int));
        }


        Int  = Mathf.Min(Mathf.Max(Int + truncated, 0), Max);

        Will = Mathf.Min(Will, Max - Int);

        Will = Mathf.Max(Will, 1);

               */
    }

    public void Rest()
    {
        Debug.Log(" rest");
        Int = 0;
        Will = Max;
    }
    public void AddWill(int amount)
    {
        //will is capped by the max-current int
        //e.g. a unit can have 5 resources but has 3 int, then will can never be larger than 3
        Will = Mathf.Max(Mathf.Min(Will + amount, Max - Int), 0);
    }

    void Awake()
    {
        inventory = GetComponent<UnitInventory>();
    }


}
