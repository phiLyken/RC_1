using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class UnitEffect_BuffStats : UnitEffect {

    public StatBuff  Buff ;
    
 

    public override UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {
        UnitEffect_BuffStats _cc = origin.gameObject.Instantiate(host.transform, true).GetComponent<UnitEffect_BuffStats>();


        _cc.name = Unique_ID + "_copy";
        _cc.Effect_Host = host;
        _cc.isCopy = true;
   


        return _cc;
    }



    public override string GetToolTipText()
    {
        return   GetShortHandle();
    }

    public override string GetShortHandle()
    {
        return GetModifier().ToString("+#;-#;0") + " " + UnitStats.StatToString(Buff.Type) ;
       
    }

    public override string GetNotificationText()
    {
        return GetShortHandle();
    }

    float GetModifier()
    {
        return Buff.Modifier * EffectBonus;
    }

}

[System.Serializable]
public class StatBuff
{
    public StatType Type;
    public float Modifier;
    
}
