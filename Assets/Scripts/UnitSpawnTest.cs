using UnityEngine;
using System.Collections;

public class UnitSpawnTest : MonoBehaviour {

    public ScriptableUnitConfig UnitConfig;
    public int group;

    void Start()
    {
        Unit u = UnitFactory.CreateUnit(UnitConfig, group, new MyMath.R_Range(2,5));

        UnitFactory.SpawnUnit(u, (GetComponent<Tile>()));
    }
}
