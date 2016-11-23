using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UI_AdrenalineRushBase : MonoBehaviour {

    protected  UnitStats Stats;
    public float EnableDelay;
    public Action OnRushGain;
    public Action OnRushFade;
    public bool HasRush;
    int old_adr;

    public void Init(UnitStats unit_stats)
    {
        Stats = unit_stats;
        Stats.OnStatUpdated += OnUpdateStat;
        old_adr = GetAdr();
        UpdateAdrenaline(GetAdr());
        UpdateBonus(GetBonus());
    }

 
    protected void OnUpdateStat(Stat s)
    {
        int threshold = Constants.ADRENALINE_RUSH_THRESHOLD;
        if (s.StatType == StatType.adrenaline)
        {
            if (gameObject != null && gameObject.activeSelf)
            {

                int _currentAdrenaline  = GetAdr();
                HasRush = _currentAdrenaline >= threshold;
                if (old_adr < threshold && HasRush )
                {
                    this.ExecuteDelayed(EnableDelay, () =>
                   {
                       if (OnRushGain != null)
                           OnRushGain();

                       RushGain();
                   }
                    );
                    
                }

                if(old_adr >= threshold && !HasRush)
                {
                    if (OnRushFade != null)
                        OnRushFade();

                    RushLoss();
                }
                old_adr = _currentAdrenaline;
                UpdateAdrenaline(_currentAdrenaline);
                UpdateBonus(GetBonus());
            }
        }
    }

    int GetAdr()
    {
        return (int) Stats.GetStatAmount(StatType.adrenaline);
    }

    int GetBonus()
    {
        return Constants.GetAdrenalineBonus(Stats);
    }
    protected virtual void UpdateAdrenaline(int adrenaline)
    {                   
                  
    }
    protected virtual void UpdateBonus(int _bonus)
    {

    }
    protected virtual void RushGain()
    {

    }

    protected virtual void RushLoss()
    {

    }

    void OnDestroy()
    {
        if(Stats != null)
        Stats.OnStatUpdated -= OnUpdateStat;
    }

   

    
}
