using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class UnitEffect_ModifyStat : UnitEffect
{

    public StatType type;
   
    public float Percent;

    public int Absolute;
        
    float _baked;

    int GetBaked()
    {
        int value = Absolute;

        if(Percent != 0)
        {
            int _base = (int) Effect_Host.Stats.GetStatAmount(type);
            value = (int) ( _base * Percent );
        }
         
        return value * EffectBonus; 
    }

    float GetBase()
    {
       return (Percent != 0) ? (Percent) : Absolute;
    }
    
    public override string GetShortHandle()
    {
        return GetToolTipText();
    }

    
    public override string GetToolTipText()
    {
        int _mod_abs = (int)( Absolute * EffectBonus);
        int _mod_percent = (int) (Percent * 100 * EffectBonus);

        string amount = (Percent != 0) ? _mod_percent.ToString("##.0") : _mod_abs.ToString("+#;-#;0");

        return amount + " "+ UnitStats.StatToString(type);
    }

    public override string GetNotificationText()
    {
        return GetToolTipText();
    }
    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {
        UnitEffect_ModifyStat _cc = (UnitEffect_ModifyStat) origin;
        _cc.isCopy = true;
        _cc._baked = GetBaked();
        return _cc.MemberwiseClone() as UnitEffect_ModifyStat;
    }

    protected  override void EffectTick()
    {
        Ticked();
        if (!Effect_Host.IsDead()) {
           Effect_Host.Stats.GetStat(type).SetAmountDelta( Effect_Host.Stats, _baked);
           
        }
    }

}