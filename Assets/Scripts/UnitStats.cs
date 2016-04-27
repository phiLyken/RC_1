using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class StatInfo
{
    public UnitStats.Stats Stat;
    public float Amount;
}

[System.Serializable]
public class StatConfig
{
    public EventHandler OnStatUpdated;

    public UnitStats.Stats Stat;
    public float max;
    public float current_max;
    public float current;
    public float start;
    public float GetProgress()
    {
        return current / current_max;
    }
    public void ModifyStat(float val)
    {

        current = Mathf.Clamp(current + val, 0, current_max);
        if (OnStatUpdated != null) OnStatUpdated();
    }
}


public class UnitStats : MonoBehaviour {
    public EventHandler OnStatUpdated;
    public StatConfig[] m_Stats;

    void Awake()
    {
        foreach (StatConfig c in m_Stats) {
            c.OnStatUpdated += OnStatUpdated;
        }
    }

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
