using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_AdrenalineRushBase : MonoBehaviour {

    UnitStats Stats;

    public bool HasRush;

    public void Init(UnitStats unit_stats)
    {
        Stats = unit_stats;
        Stats.OnStatUpdated += OnUpdateStat;

        UpdateBonus(GetIntBonus());
    }

 
    protected void OnUpdateStat(Stat s)
    {
        if (s.StatType == StatType.adrenaline)
        {
            if (gameObject != null && gameObject.activeSelf)
            {

                int b  = GetIntBonus();
                HasRush = b > 1;
                UpdateBonus(b);
            }
        }
    }

    int GetIntBonus()
    {
        return Constants.GetAdrenalineRushBonus(Stats);
    }

    protected virtual void UpdateBonus(int _bonus)
    {                   
                  
    }

    void OnDestroy()
    {
        Stats.OnStatUpdated -= OnUpdateStat;
    }

   

    
}
