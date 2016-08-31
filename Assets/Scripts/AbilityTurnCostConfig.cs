using UnityEngine;
using System.Collections;

[System.Serializable]
public class AbilityTurnCostConfig
{
    public bool useStat;
    public StatType costStat;

    public int fixedCost;

    Unit m_unit;

    public void Init(Unit u)
    {
        m_unit = u;
    }

    public float GetCost()
    {
        return useStat ? m_unit.Stats.GetStatAmount(costStat) : fixedCost;
    }
}

