using UnityEngine;
using System.Collections;

public class UnitSpawnTest : MonoBehaviour {

    public UnitConfig Config;

    void Start()
    {
        Unit u = UnitFactory.CreateUnit(Config);

        UnitFactory.SpawnUnit(u, (GetComponent<Tile>()));
    }
}
