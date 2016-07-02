using UnityEngine;
using System.Collections;

public class UnitSpawnTest : MonoBehaviour {

    public ScriptableUnitConfig UnitConfig;
    public int group;

    void Start()
    {
        Unit u = UnitFactory.CreateUnit(UnitConfig, group,0);

        UnitFactory.SpawnUnit(u, (GetComponent<Tile>()));
    }
}
