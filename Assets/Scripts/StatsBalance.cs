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

    public float GetValueForStat(StatType type, int perk_level)
    {
        foreach(StatLevelConfig config in StatConfigs)
        {
            foreach(SubStatConfig sub_config in config.SubConfigs)
            {
                if(sub_config.SubStat == type)
                {
                    return sub_config.Values[perk_level];
                }
            }
        }

        return 0;
    }
    public float GetValueForStat(StatType type, UnitStats stats)
    {
      
        foreach(StatLevelConfig config in StatConfigs)
        {
            StatType root_type = config.Perk;

            if (root_type == type)
            {
                Debug.Log("perk value " + type);
                return stats.GetStatAmount(type);
            }

            foreach(SubStatConfig sub_config in config.SubConfigs)
            {
                
                if(sub_config.SubStat == type)
                {                   

                    int current_level_root = (int) stats.GetStatAmount(root_type);

                    return sub_config.Values[Mathf.Min(current_level_root, sub_config.Values.Length-1)];
                   
                }
            }
        }

        return 0;
        ;
    }
}

[System.Serializable]
public class StatLevelConfig
{
    public StatType Perk;
    public List<SubStatConfig> SubConfigs;
}

[System.Serializable]
public class SubStatConfig
{
    public StatType SubStat;
    public float[] Values;
}