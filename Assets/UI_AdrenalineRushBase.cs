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
   

            int _currentAdrenaline  = GetAdr();
            HasRush = _currentAdrenaline >= threshold;

            if (old_adr < threshold && HasRush )
            {
                if (isActiveAndEnabled)
                { 
                    this.ExecuteDelayed(EnableDelay,  TriggerRush );                        
                    
                } else
                {
                    TriggerRush();
                }
            }
            if (old_adr >= threshold && !HasRush)
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

    void TriggerRush()
    {
        if (HasRush)
        {
            if (OnRushGain != null)
            {
                OnRushGain();
            }
            RushGain();
        }

    }
    int GetAdr()
    {
        return (int) Stats.GetStatAmount(StatType.adrenaline);
    }

    float GetBonus()
    {
        return Constants.GetAdrenalineBonus(Stats);
    }
    protected virtual void UpdateAdrenaline(int adrenaline)
    {                   
                  
    }
    protected virtual void UpdateBonus(float _bonus)
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
