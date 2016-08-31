using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class UnitEffect_BuffStats : UnitEffect {

    public StatBuff  Buff ;
    
    public override UnitEffect MakeCopy(UnitEffect original, Unit host)
    {
        return (UnitEffect_BuffStats)((UnitEffect_BuffStats) original).MemberwiseClone();
    }

    public override string GetString()
    {
        return Buff.Type + " " + Buff.Modifier;
    }

}

[System.Serializable]
public class StatBuff
{
    public StatType Type;
    public float Modifier;
}
