using UnityEngine;
using System.Collections;

public class UnitStats_Test : MonoBehaviour {

    public UnitBaseStats baseStats;

    public  UnitStats m_stats;

    public void MakeStats(UnitBaseStats stats)
    {
        
        if(m_stats == null)
        {
         m_stats =    gameObject.AddComponent<UnitStats>(); 
        }

        m_stats.Init(StatsHelper.GetStatListForInit(baseStats), null);
    }
}
