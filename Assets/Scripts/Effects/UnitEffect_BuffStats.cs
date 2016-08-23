using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class UnitEffect_BuffStats : UnitEffect {

    public StatBuff[] Buffs;
    
    public override UnitEffect MakeCopy(UnitEffect original, Unit host)
    {
        return (UnitEffect_BuffStats)((UnitEffect_BuffStats) original).MemberwiseClone();
    }


}

[System.Serializable]
public class StatBuff
{
    public StatType Type;
    public float Modifier;
}
