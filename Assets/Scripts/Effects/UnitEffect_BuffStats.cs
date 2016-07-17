using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class UnitEffect_BuffStats : UnitEffect {

    public StatInfo[] Buffs;



    public override UnitEffect MakeCopy(UnitEffect origin)
    {
        return (UnitEffect_BuffStats)((UnitEffect_BuffStats)origin).MemberwiseClone();
    }


}
