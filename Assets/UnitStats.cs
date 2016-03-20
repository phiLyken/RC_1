using UnityEngine;
using System.Collections;


[System.Serializable]
public class StatInfo
{
    public UnitStats.Stats Stat;
    public float Amount;
}

[System.Serializable]
public class StatConfig
{
    public UnitStats.Stats Stat;
    public float max;
    public float current;
    public float start;
}


public class UnitStats : MonoBehaviour {

    public StatConfig[] m_Stats;

    public StatConfig GetStat(Stats stat)
    {
        foreach(StatConfig c in m_Stats) { if (c.Stat == stat) return c; }
        return null;
    }

    public enum Stats
    {
        will, intensity, movement_range
    }    
}
