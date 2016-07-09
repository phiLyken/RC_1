using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class UnitEffect_ModifyStat : UnitEffect
{

    public UnitStats.StatType type;
   
    public float Percent;

    public int Absolute;

    int GetAmount()
    {
        int _base = (int) Effect_Host.Stats.GetStat(type).Amount;

        int value = Absolute;

        if(Percent != 0)
        {
            value = (int) ( _base * Percent );
        }
        Debug.Log("AMOUNT " + value +"  "+_base +" "+Percent);
        return value;
    }

    public UnitEffect_ModifyStat(UnitEffect_ModifyStat origin) : base(origin)
    {
        Percent = origin.Percent;
        Absolute = origin.Absolute;
        type = origin.type;
    }

    public override string GetString()
    {
        return GetAmount()+" "+ type;
    }

    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    protected override IEnumerator ApplyEffect(Unit target, UnitEffect effect)
    {
        //Make copy
        UnitEffect_ModifyStat copy = new UnitEffect_ModifyStat(effect as UnitEffect_ModifyStat);
        copy.Effect_Host = target;
        TurnSystem.Instance.OnGlobalTurn += copy.OnGlobalTurn;
        if (target.GetComponent<Unit_EffectManager>().ApplyEffect(copy)) { 
         
   
            copy.EffectTick();
        }

        yield return null;
    }

    void EffectTick()
    {
        Ticked();
        if (!Effect_Host.IsDead())
            Effect_Host.Stats.GetStat(type).Amount += GetAmount() ;
    }

    public override void SetPreview(UI_DmgPreview prev, Unit target)
    {
    }

    protected override void GlobalTurnTick()
    {
        Ticked();
      
  
    }

}