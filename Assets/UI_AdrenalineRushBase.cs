using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UI_AdrenalineRushBase : MonoBehaviour {

    UnitStats Stats;
    public float EnableDelay;
    public Action OnRushGain;
    public Action OnRushFade;
    public bool HasRush;
    int old_rush;

    public void Init(UnitStats unit_stats)
    {
        Stats = unit_stats;
        Stats.OnStatUpdated += OnUpdateStat;
        old_rush = GetIntBonus();
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
                if (old_rush <= 1 && HasRush )
                {
                    this.ExecuteDelayed(EnableDelay, () =>
                   {
                       if (OnRushGain != null)
                           OnRushGain();

                       RushGain();
                   }
                    );
                    
                }

                if(old_rush > 1 && !HasRush)
                {
                    if (OnRushFade != null)
                        OnRushFade();

                    RushLoss();
                }
                old_rush = b;
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
