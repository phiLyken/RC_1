﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitStats : MonoBehaviour
{
    public EventHandler OnStatUpdated;
    public EventHandler OnHPDepleted;

    [HideInInspector]
    List<Stat> CurrentStats;
  
    Unit_EffectManager m_Effects;
        
    public static string StatToString(StatType t)
    {
        switch (t)
        {
            case StatType.adrenaline:
                return "Adrenaline";
            case StatType.oxygen:
                return "Oxygen";
            default:
                return t.ToString();
        }
    }

    public void Init(List<Stat> stats, Unit_EffectManager effect_manager)
    {
        CurrentStats = stats;

        m_Effects = effect_manager;

        if(effect_manager != null)
        {
            effect_manager.OnEffectAdded += s => { UpdatedBuffs(); };
            effect_manager.OnEffectRemoved += s => { UpdatedBuffs(); };
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
            Debug.Log("Stat not foudn " + type);
            return 0;
        }

        float base_value = stat.GetAmount(this);

        float buffed = GetBuffAmountForStat(type);

        return base_value + buffed;
    }

    public void SetStatAmount(StatType type, float new_value)
    {
        float old_value = GetStat(type).GetAmount(this);

        GetStat(type).SetAmount(new_value);

        if(old_value != new_value) { 
            Updated();
        }
    }
    protected void Updated()
    {
        if (OnStatUpdated != null) OnStatUpdated();          
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
                           buffs.AddRange(((UnitEffect_BuffStats) eff).Buffs);
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
        int dmg_received = (-(dmg.GetDamage()));
        int int_received = Constants.GetGainedAdrenaline( this, dmg_received);

        Debug.Log(this.name + " rcv damge " + dmg_received + "  rcvd multiplier:" + "WTF" + "  +int=" + int_received);

        AddWill(dmg_received);

        int x, y = 0;
        AddInt(int_received, true, out x, out y);

        if (GetStatAmount(StatType.oxygen) <= 0 && OnHPDepleted != null)
        {
            OnHPDepleted();
        }
    }

    public void AddInt(int amount, bool consumeWill, out int removed_will, out int added_int)
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

        added_int = _new_int - Int;

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
        int Int = (int) GetStatAmount(StatType.adrenaline);
        int Will = (int) GetStatAmount(StatType.oxygen);

        SetStatAmount(StatType.adrenaline, Int - Mathf.Max((Int + Will) - Max, 0));

    }
    public void Rest()
    {
        Debug.Log(" rest");
        SetStatAmount(StatType.adrenaline, 0);
        SetStatAmount(StatType.oxygen,(int) GetStatAmount(StatType.vitality));
    }

    public void AddWill(int amount)
    {
        int Max = (int) GetStatAmount(StatType.vitality);
        int Int = (int) GetStatAmount(StatType.adrenaline);
        int Will = (int) GetStatAmount(StatType.oxygen);
        //will is capped by the max-current int
        //e.g. a unit can have 5 resources but has 3 int, then will can never be larger than 3
        SetStatAmount(StatType.oxygen, Mathf.Max(Mathf.Min(Will + amount, Max - Int), 0));
    }
}

