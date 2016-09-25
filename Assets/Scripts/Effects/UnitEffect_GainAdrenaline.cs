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

    public override UnitEffect MakeCopy(UnitEffect origin, Unit effect_host)
    { 
        UnitEffect_GainAdrenaline stim = (UnitEffect_GainAdrenaline)( (UnitEffect_GainAdrenaline) origin).MemberwiseClone();
        stim.baked_adrenaline = Constants.GetGainedAdrenaline(effect_host, Rolls) ;
        
        return stim;
    }

    protected override void EffectTick()
    {
        Debug.Log("Ticked Adrenealin Baked Adrenaline " + baked_adrenaline);
        Effect_Host.Stats.AddInt(baked_adrenaline, ConsumeWill, out removed, out added);
        Ticked();
    }

    public override string GetToolTipText()
    {
            
      string str =   "Infuses adrenaline.";
        if (ConsumeWill)
            str += "\nConsumes Oxygen if the units stamina is exceeded.";

        return str;
        
    }

    public override string GetShortHandle()
    {
        return "Rage";
    }


}
