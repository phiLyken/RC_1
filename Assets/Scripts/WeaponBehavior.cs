using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponBehavior :   MonoBehaviour {

    public Sprite Icon;
    public int TimeDelay;
    public int Range;
    public List<  UnitEffect > Effects;
    public IntBonus IntBonus;

    public UseStat RangeModifier;

    public int GetRange()
    {
        return Range;
    }
}


