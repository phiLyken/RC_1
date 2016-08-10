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

    int GetAmount()
    {
        int _base = (int) Effect_Host.Stats.GetStatAmount(type);

        int value = Absolute;

        if(Percent != 0)
        {
            value = (int) ( _base * Percent );
        }
        Debug.Log("AMOUNT " + value +"  "+_base +" "+Percent);
        return value;
    }


    public override string GetString()
    {
        return GetAmount()+" "+ UnitStats.StatToString(type);
    }

    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override UnitEffect MakeCopy(UnitEffect origin)
    {
        UnitEffect_ModifyStat _cc = (UnitEffect_ModifyStat) origin;
        return _cc.MemberwiseClone() as UnitEffect_ModifyStat;
    }

    protected  override void EffectTick()
    {
        Ticked();
        if (!Effect_Host.IsDead()) {
            Effect_Host.Stats.GetStat(type);
           
        }
    }

}