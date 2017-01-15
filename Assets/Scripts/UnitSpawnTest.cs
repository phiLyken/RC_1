using UnityEngine;
using System.Collections;

public class UnitSpawnTest : MonoBehaviour {

    public ScriptableUnitConfig UnitConfig;
    public int group;

    void Start()
    {
        Unit u = UnitFactory.CreateUnit(UnitConfig, group, new M_Math.R_Range(2,5), true);

        UnitFactory.SpawnUnit(u, (GetComponent<Tile>()));
    }
}
