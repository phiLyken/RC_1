using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class UnitEffect_ModifyStat : UnitEffect
{

    public StatType type;
   
  
    public float Percent;
    public StatType percent_of;
    public bool ApplyDiff;

    public int Absolute;
        
    float _baked;


    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {
        UnitEffect_ModifyStat _cc_modstat = base.MakeCopy(origin, host) as UnitEffect_ModifyStat;
        _cc_modstat._baked = _cc_modstat.GetBaked();
        return _cc_modstat;
    }


    int GetBaked()
    {
        int value = Absolute;

        if(Percent != 0)
        {
            int _base = (int) Effect_Host.Stats.GetStatAmount(percent_of);
            value = (int) ( _base * Percent );
        }

        value  = Mathf.RoundToInt( EffectBonus * value);

        if (ApplyDiff)
        { 
             int old_value = (int) Effect_Host.Stats.GetStatAmount(type);
            return value - old_value;
        }
        return  value;
      
    }

 
    
    public override string GetShortHandle()
    {
        return GetToolTipText();
    }

    
    public override string GetToolTipText()
    {

        int _mod_abs = (int)( Absolute * EffectBonus );
        int _mod_percent = (int) (Percent * 100 * EffectBonus);

        string amount = (Percent != 0) ? (_mod_percent.ToString("+#;-#;0") +"%") : _mod_abs.ToString("+#;-#;0");

        return amount + " "+ UnitStats.StatToString(type);
    }

    public override string GetNotificationText()
    {
 

        return _baked.ToString("+#;-#;0") + " " + UnitStats.StatToString(type);
    }


    protected  override void EffectTick()
    {
        int x, y;
        if (!Effect_Host.IsDead())
        {

            switch (type)
            {
                case StatType.adrenaline:
                    Effect_Host.Stats.AddAdrenaline( (int) _baked, true, out   x, out   y);
                    break;
                case StatType.oxygen:
                    Effect_Host.Stats.AddOxygen( (int) _baked, true);
                    break;
                default:
                    Effect_Host.Stats.SetStatAmountDelta(type, _baked);
                    break;

            }
           

        }
        Ticked();
        
    }

}