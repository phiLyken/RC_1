using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UnitEffect_stim : UnitEffect
{

    public MyMath.R_Range AdrenalineAmount;

    int baked_adrenaline = -1;
    int removed = -1;
    int added = -1;

    public override UnitEffect MakeCopy(UnitEffect origin)
    {
        UnitEffect_stim stim = (UnitEffect_stim)( (UnitEffect_stim) origin).MemberwiseClone();
        stim.baked_adrenaline = (int) stim.AdrenalineAmount.Value() ;
        return stim;
    }

    protected override void EffectTick()
    {

        PlayerUnitStats stats = Effect_Host.Stats as PlayerUnitStats;
               
        stats.AddInt(baked_adrenaline, true, out removed, out added);
        Ticked();
    }

    public override string GetString()
    {
        string str = "+" + added+" A";
        if(removed > 0)
        {
            str += " -" + removed +" O";
        }
        return str;
        
    }
}
