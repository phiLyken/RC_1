using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UI_AdrenalineRushBase : MonoBehaviour {

    protected  UnitStats Stats;
    public float EnableDelay;
    public System.Action OnRushGain;
    public System.Action OnRushFade;
    public bool HasRush;
    int old_adr;

    public void Init(UnitStats unit_stats)
    {
        Stats = unit_stats;
        Stats.OnStatUpdated += OnUpdateStat;
        old_adr = GetAdr();
       // UpdateAdrenaline(GetAdr(), false);
       // UpdateBonus(GetBonus(), false);

        UpdateAdr(false);
        
    }


    void UpdateAdr( bool useDelay)
    {
        int threshold = Constants.ADRENALINE_RUSH_THRESHOLD;
        int _currentAdrenaline = GetAdr();
        HasRush = _currentAdrenaline >= threshold;

        if (old_adr < threshold && HasRush)
        {
            if (isActiveAndEnabled && useDelay)
            {
                this.ExecuteDelayed(EnableDelay, TriggerRush);

            }
            else
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
        UpdateAdrenaline(_currentAdrenaline, useDelay);
        UpdateBonus(GetBonus(), useDelay);
    }
 
    protected void OnUpdateStat(Stat s)
    {       
        if (s.StatType == StatType.adrenaline)
        {
            UpdateAdr(true);                
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
    protected virtual void UpdateAdrenaline(int adrenaline, bool useDelay)
    {                   
                  
    }
    protected virtual void UpdateBonus(float _bonus, bool useDelay)
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
