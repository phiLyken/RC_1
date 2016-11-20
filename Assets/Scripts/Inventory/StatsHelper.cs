using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class StatsHelper
{

    public static List<Stat> GetStatListForInit(UnitBaseStats stats)
    {
        return GetStatListForInit(stats.Survival, stats.Mobility, stats.Tech, stats.Combat, stats.Focus, stats.StartAdrenaline, stats.StartOxyGen, stats.StartTurnTime);
    }

    public static List<Stat> GetStatListForInit(int perk_survival, int perk_mobility, int perk_tech, int perk_combat, int perk_focus, int start_adr, int start_oxygen, MyMath.R_Range current_delay)
    {
        List<Stat> stats = new List<Stat>();

        //PERKS
        stats.Add(new ModifiableStat(StatType.survival, perk_survival));
        stats.Add(new ModifiableStat(StatType.mobility, perk_mobility));
        stats.Add(new ModifiableStat(StatType.tech, perk_tech));
        stats.Add(new ModifiableStat(StatType.combat, perk_combat));
        stats.Add(new ModifiableStat(StatType.focus, perk_focus));
       
        //RUNTIME STATS
        stats.Add(new ModifiableStat(StatType.oxygen,  Mathf.Max(1, start_oxygen)));
        stats.Add(new ModifiableStat(StatType.adrenaline, start_adr));
        stats.Add(new ModifiableStat(StatType.current_turn_time, current_delay.Value()));

        //DUMMY STATS
        stats.Add(new Stat(StatType.vitality));
        stats.Add(new Stat(StatType.rest_charges_max));
        stats.Add(new Stat(StatType.move_range));
        stats.Add(new Stat(StatType.move_time_delay));
        stats.Add(new Stat(StatType.attack_special_charges_max));
        stats.Add(new Stat(StatType.attack_special_delay));
        stats.Add(new Stat(StatType.attack_extra_damage_min));
        stats.Add(new Stat(StatType.attack_extra_damage_max));
        stats.Add(new Stat(StatType.attack_normal_power));
        stats.Add(new Stat(StatType.attack_normal_delay));
        stats.Add(new Stat(StatType.adrenaline_conversion_min));
        stats.Add(new Stat(StatType.adrenaline_conversion_max));
        stats.Add(new Stat(StatType.stimpack_charges_max));

        return stats;
    }
}

    public enum StatType
    {
        //survivability
        survival,
        vitality,
        rest_charges_max,

        //mobility
        mobility,
        move_range,
        move_time_delay,

        //technical
        tech,

        attack_special_delay,
        attack_special_charges_max,

        //combat
        combat,
        attack_normal_power,
        attack_normal_delay,
        attack_range_modifier,
        attack_extra_damage_min,
        attack_extra_damage_max,

        //focus
        focus,
        adrenaline_conversion_min,
        adrenaline_conversion_max,
        stimpack_charges_max,


        //run time stats
        oxygen,
        adrenaline,
        current_turn_time
    }

[System.Serializable]
public class UnitBaseStats
{
    public int Survival;
    public int Mobility;
	public int Focus;
    public int Combat;
	public int Tech;
    public int StartOxyGen;
    public int StartAdrenaline;

    public MyMath.R_Range StartTurnTime;
}

[System.Serializable]
public class StatInfo
{
    public StatType StatType;
    public float Value;
}

[System.Serializable]
public class UseStat
{
    public bool Use;
    public StatType StatType;
    public float DefaultValue;

    public float GetValue(Unit u)
    {
        return Use ? u.Stats.GetStatAmount(StatType) : DefaultValue;
    }

}
[System.Serializable]
public class Stat
{
    public StatType StatType;

    public virtual float GetAmount(UnitStats stats)
    {
        float base_value = StatsBalance.GetStatsBalance().GetValueForStat(StatType, stats);
        return base_value;
    }

    public Stat(StatType type)
    {
        StatType = type;
    }
        
    public virtual void SetAmount(float new_value)
    {
            
    }

 
}

   


 public class ModifiableStat : Stat
{
    private float amount;

    public ModifiableStat(StatType type, float startAmount) : base(type)
    {
        amount = startAmount;
    }

    public override void SetAmount(float new_value)
    {
        
        Debug.Log(StatType.ToString() + " " + new_value);
        amount = new_value;
    }

    public override float GetAmount(UnitStats stats)
    {
        return amount;
    }
}

