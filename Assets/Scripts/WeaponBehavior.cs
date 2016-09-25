using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponBehavior :   MonoBehaviour {

    public string ActionID;
    public StatInfo[] Requirements;

    public Sprite Icon;
    public int TimeDelay;
 
    public List<  UnitEffect > Effects;
    public IntBonus IntBonus;

    public TargetInfo TargetRule;

    public string TileViewState;

    public float GetRange(Unit u)
    {
        return TargetRule.GetRange(u);
    }

    public TargetHighLight PreviewPrefab;
}


