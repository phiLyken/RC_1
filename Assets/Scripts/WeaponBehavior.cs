using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponBehavior :   MonoBehaviour {

    public StatInfo[] Requirements;

    public Sprite Icon;
    public int TimeDelay;
    public List<  UnitEffect > Effects;
    public IntBonus IntBonus;
}


