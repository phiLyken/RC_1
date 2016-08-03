using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsBalance : MonoBehaviour {

    public int[] DustCostToLevel;

    public List<StatLevelConfig> StatConfigs;

   
    public static StatsBalance GetStatsBalance()
    {  
        return (Resources.Load("Units/StatsBalance") as GameObject).GetComponent<StatsBalance>();
    }

    public int GetCostForLevel(int level)
    {
        return DustCostToLevel[ Mathf.Min(level, DustCostToLevel.Length)];
    }

    public int GetLevelForStat(UnitStats.StatType type, PlayerUnitStats stats)
    {
      
        foreach(StatLevelConfig config in StatConfigs)
        {
            if(config.RootStat == type)
            {
                return stats.GetStatAmount(type);
            }

            foreach(SubStatConfig sub_config in config.SubConfigs)
            {
                
                if(sub_config.SubStat == type)
                {
                    UnitStats.StatType root_type = config.RootStat;

                    int current_level_root = stats.GetStatAmount(root_type);

                    return sub_config.Values[current_level_root];
                   
                }
            }
        }

        return stats.GetStatAmount(type);
    }
}

[System.Serializable]
public class StatLevelConfig
{
    public UnitStats.StatType RootStat;
    public List<SubStatConfig> SubConfigs;
}

[System.Serializable]
public class SubStatConfig
{
    public UnitStats.StatType SubStat;
    public int[] Values;
}