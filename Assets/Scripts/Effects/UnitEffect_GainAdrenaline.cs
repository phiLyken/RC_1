using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UnitEffect_GainAdrenaline : UnitEffect
{

    public bool ConsumeWill;
    public int Rolls;

    int baked_adrenaline = -1;
    int removed = -1;
    int added = -1;

    public override UnitEffect MakeCopy(UnitEffect origin)
    {
        UnitEffect_GainAdrenaline stim = (UnitEffect_GainAdrenaline)( (UnitEffect_GainAdrenaline) origin).MemberwiseClone();
        stim.baked_adrenaline = Constants.GetGainedAdrenaline(Effect_Host, Rolls) ;
        return stim;
    }

    protected override void EffectTick()
    {     

        Effect_Host.Stats.AddInt(baked_adrenaline, ConsumeWill, out removed, out added);
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
