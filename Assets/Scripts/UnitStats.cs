using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void StatEventHandler(Stat stat);

public class UnitStats : MonoBehaviour
{
    public StatEventHandler OnStatUpdated;
    public EventHandler OnHPDepleted;
    public EventHandler OnDmgReceived;
    [HideInInspector]
    List<Stat> CurrentStats;
 
    Unit_EffectManager m_Effects;
        
    public static string StatToString(StatType t)
    {
        switch (t)
        {
            case StatType.move_range:
                return "Move Range";
            case StatType.move_time_delay:
                return "Move Time Cost";
            case StatType.adrenaline:
                return "Adrenaline";
            case StatType.oxygen:
                return "Oxygen";
            case StatType.attack_extra_damage_max:
            case StatType.attack_extra_damage_min:
                return "+ATK DMG";

            case StatType.adrenaline_conversion_max:
            case StatType.adrenaline_conversion_min:
                return "Adr. Conversion";
            case StatType.current_turn_time:
                return "Turn Delay";
            case StatType.attack_normal_delay:
                return "Attack Time Cost";
            default:
                return t.ToString();
        }
    }

    public void Init(List<Stat> stats, Unit_EffectManager effect_manager )
    {
        CurrentStats = stats;

        m_Effects = effect_manager;

        if(effect_manager != null)
        {
            effect_manager.OnEffectAdded += s => { UpdatedBuffs(); };
            effect_manager.OnEffectRemoved += s => { UpdatedBuffs(); };
        }

        OnStatUpdated += CheckHP;

        
    }

    void CheckHP(Stat t)
    {

        if (t.StatType == StatType.oxygen && GetStatAmount(StatType.oxygen) <= 0 && OnHPDepleted != null)
        {
    
            OnStatUpdated -= CheckHP;
       
            OnHPDepleted();
            OnStatUpdated = null;
        }
    }

    public Stat GetStat(StatType type)
    {
        foreach (Stat s in CurrentStats) if (s.StatType == type) return s;
       
        return null;
    }
    
    public float GetStatAmount(StatType type)
    {
        Stat stat = GetStat(type);
        if(stat == null)
        {
           // MDebug.Log("Stat not foudn " + type);
            return 0;
        }

        float base_value = stat.GetAmount(this);

        float buffed = GetBuffAmountForStat(type);

        return base_value + buffed;
    }

    public void SetStatAmount(StatType type, float new_value)
    {
        float old_value = GetStat(type).GetAmount(this);

        Stat s =  GetStat(type);

 
         s.SetAmount( new_value);
 
 
        if (old_value != new_value) {            
            Updated(s);
        }
    }
    
    public void SetStatAmountDelta(StatType type, float delta)
    {
        SetStatAmount(type, GetStatAmount(type) + delta);
    }

    protected void Updated(Stat stat)
    {
        if (OnStatUpdated != null   ) OnStatUpdated(stat);          
    }

    float GetBuffAmountForStat(StatType type)
    {            
        if (m_Effects != null)
        {
            List<StatBuff> buffs = new List<StatBuff>();
            m_Effects.ActiveEffects.ForEach(
                   eff =>
                   {
                       if (eff.GetType() == typeof(UnitEffect_BuffStats))
                       {
                           buffs.Add(((UnitEffect_BuffStats) eff).Buff);
                       }
                   }
             );

            if (buffs.Count > 0)
            {
                return buffs.Where(st => st.Type == type).ToList().Sum(buff => buff.Modifier);
            }
        }
        return 0;
    }

    protected  void UpdatedBuffs()
    {
        MaxUpdated();
    }

    public void ReceiveDamage(UnitEffect_Damage dmg)
    {
        if (GetStatAmount(StatType.oxygen) <= 0)
            return;

        int dmg_received = (-(dmg.GetDamage()));
        int int_received = Constants.GetGainedAdrenaline( this, Mathf.Abs( dmg_received ));

        MDebug.Log("^effects "+this.name + " rcv damge " + dmg_received + "  rcvd multiplier:" + "WTF" + "  +int=" + int_received);

        AddOxygen(dmg_received, true);

        if (dmg_received < 0 && OnDmgReceived != null)
        { 
            OnDmgReceived();

        }

        
        int x, y = 0;
        
        if(GetStatAmount(StatType.oxygen) > 0)
            AddAdrenaline(int_received, true, out x, out y);

    }

    public void AddAdrenaline(int amount, bool consumeWill, out int removed_will, out int added_int)
    {
        removed_will = 0;
        added_int = 0;

        int Max = (int)GetStatAmount(StatType.vitality);
        int Int = (int)GetStatAmount(StatType.adrenaline);
        int Will = (int)GetStatAmount(StatType.oxygen);

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

        added_int = Mathf.Max( _new_int - Int, 0);

        SetStatAmount(StatType.adrenaline, _new_int);

        //in case int has been increased  more than the cap, "will" will be reduced

        int _new_will = Mathf.Min(Will, Max - _new_int);
        removed_will = Will - _new_will;
        SetStatAmount(StatType.oxygen, _new_will);

    }

    //recalc INT / WILL based on max
    public void MaxUpdated()
    {
        int Max = (int) GetStatAmount(StatType.vitality);
        int Adrenaline = (int) GetStatAmount(StatType.adrenaline);
        int Oxygen = (int) GetStatAmount(StatType.oxygen);


        int new_oxygen = Mathf.Min(Max, Oxygen);
        int new_adrenaline = Mathf.Min(Adrenaline, Max - new_oxygen);
         
        SetStatAmount(StatType.adrenaline, new_adrenaline);
        SetStatAmount(StatType.oxygen, new_oxygen);

    }
    public void Rest()
    {
        MDebug.Log(" rest");
        SetStatAmount(StatType.adrenaline, 0);
        SetStatAmount(StatType.oxygen,(int) GetStatAmount(StatType.vitality));
    }

    public void AddOxygen(int amount, bool remove_adr)
    {
        int Max = (int) GetStatAmount(StatType.vitality);
        int Int = (int) GetStatAmount(StatType.adrenaline);
        int Will = (int) GetStatAmount(StatType.oxygen);
        //will is capped by the max-current int
        //e.g. a unit can have 5 resources but has 3 int, then will can never be larger than 3

        if (remove_adr)
        {

            int free = Max - (Int + Will);
            int adr_removed = Mathf.Min(Int, Mathf.Max(amount - free, 0));
            SetStatAmount(StatType.adrenaline, Int - adr_removed);
            Int -= adr_removed;

        }  

         SetStatAmount(StatType.oxygen, Mathf.Max(Mathf.Min(Will + amount, Max - Int), 0));
     
    }
}

