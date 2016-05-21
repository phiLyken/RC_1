using UnityEngine;
using System.Collections;

public class ScriptableUnitConfig : ScriptableObject
{
    public void CopyFromConfig(UnitConfig c)
    {
        ID = c.ID;
        StatType = c.StatType;
        stats = c.stats;
        Actions = c.Actions;

        Owner = c.Owner;
        Mesh = c.Mesh;
        UnitPower = c.UnitPower;

    }

    public string ID;
    public StatType StatType;
    public StatInfo[] stats;

    public UnitActionBase[] Actions;

    public int Owner;
    public int TriggerRange;

    public GameObject Mesh;

    public int UnitPower;
}