using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
    public virtual void ReceiveDamage(UnitEffect_Damage dmg)
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
        will, intensity, max, HP, move_range
    }
    

    public StatInfo GetStat(StatType type)
    {
        foreach (StatInfo s in Stats) if (s.Stat == type) return s;
        // Debug.LogWarning("Stat not found " + type);
        StatInfo si = new StatInfo();
        si.Amount = 0;
        return si;
    }


    protected void Updated()
    {
        if (OnStatUpdated != null) OnStatUpdated();
     
        
    }


}

