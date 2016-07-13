using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class StatInfo
{
    public UnitStats.StatType Stat;
    public float Amount;

    public float GetAmount()
    {
        return Amount;
    }
}

public class UnitStats : MonoBehaviour
{
    public EventHandler OnStatUpdated;
    public EventHandler OnHPDepleted;

    public StatInfo[] Stats;

    UnitInventory mInventory;
    Unit_EffectManager mEffects;

    public virtual void ReceiveDamage(UnitEffect_Damage dmg)
    {

    }

    protected virtual void UpdatedBuffs()
    {

    }

    public static string StatToString(StatType t)
    {
        switch (t)
        {
            case StatType.intensity:
                return "Adrenaline";
            case StatType.will:
                return "Oxygen";
            default:
                return t.ToString();
        }
    }
    public enum StatType
    {
        will, intensity, max, HP, move_range, current_initiative, speed
    }
    
    public StatInfo GetStat(StatType type)
    {
        foreach (StatInfo s in Stats) if (s.Stat == type) return s;
        // Debug.LogWarning("Stat not found " + type);
        StatInfo si = new StatInfo();
        si.Amount = 0;
        return si;
    }
    
    public int GetStatAmount(StatType type)
    {
        //Speed, max and move_range are non-consumable stats so their values can be temporarily modified by "buffs"
        if(type == StatType.speed || type == StatType.max ||type == StatType.move_range)
        {
            return (int) GetBuffedStat(type);
        } else { 
            return (int) GetStat(type).Amount;
        }
    }

    public void SetStatAmount(StatType type, int new_value)
    {
        int old_value = (int) GetStat(type).Amount;
        GetStat(type).Amount = new_value;

        if(old_value != new_value) { 
            Updated();
        }
    }
    protected void Updated()
    {
        if (OnStatUpdated != null) OnStatUpdated();  
        
    }

    float GetBuffedStat(StatType type)
    {
        float amount = GetStat(type).Amount;
        List<StatInfo> buffs = new List<StatInfo>();

        GetEffectManager().ActiveEffects.ForEach(
               eff =>
               {
                   if (eff.GetType() == typeof(UnitEffect_BuffStats))
                   {
                       buffs.AddRange(((UnitEffect_BuffStats)eff).Buffs);
                   }
               }
         );

        buffs.AddRange(GetInventory().GetAllBuffs());           


        if (buffs.Count > 0)
        {
            buffs = buffs.Where(st => st.Stat == type).ToList();
            amount += buffs.Sum(st => st.Amount);
        }         
        
        amount += (int)GetStat(type).Amount;
        return amount;
    }

    UnitInventory GetInventory()
    {
        if(mInventory == null)
        {
            mInventory = GetComponent<UnitInventory>();
            mInventory.OnInventoryUpdated += UpdatedBuffs;
        }

        return mInventory;
    }

    Unit_EffectManager GetEffectManager()
    {
        if (mEffects == null)
        {
            mEffects = GetComponent<Unit_EffectManager>();
            mEffects.OnEffectAdded += s => { UpdatedBuffs(); };
            mEffects.OnEffectRemoved += s => { UpdatedBuffs(); };
        }

        return mEffects;
    }

    void Awake()
    {
        GetEffectManager();
        GetInventory();
    }
}

