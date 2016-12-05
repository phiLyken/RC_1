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


    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {

        UnitEffect_GainAdrenaline _cc_adr = base.MakeCopy(origin, host) as UnitEffect_GainAdrenaline;

        _cc_adr.baked_adrenaline = Constants.GetGainedAdrenaline(host.Stats, Rolls);

        return _cc_adr;


    }
 

    protected override void EffectTick()
    {
        Debug.Log("^effects Tick" + this.ToString());
        Effect_Host.Stats.AddAdrenaline(baked_adrenaline, ConsumeWill, out removed, out added);
       // Debug.Log(removed + " " + added);
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
        return "+ " + Constants.GetRageAdrenalineMin(  ((UnitAction_ApplyEffect) Instigator).GetOwner().Stats, Rolls) +"-"+
           + Constants.GetRageAdrenalineMax(((UnitAction_ApplyEffect) Instigator).GetOwner().Stats, Rolls) +
           " " + UnitStats.StatToString(StatType.adrenaline);
        ;
    }

    public override string GetNotificationText()
    {
        return "+" + baked_adrenaline.ToString() +" "+ UnitStats.StatToString(StatType.adrenaline);
        ;
    }

    public override string ToString()
    {
        return base.ToString() + " baked_adr:" + baked_adrenaline;
    }

}
