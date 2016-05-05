using UnityEngine;
using System.Collections;

public class UnitSpawnTest : MonoBehaviour {

    public UnitConfig Config;
    public int group;

    void Start()
    {
        Unit u = UnitFactory.CreateUnit(Config, group);

        UnitFactory.SpawnUnit(u, (GetComponent<Tile>()));
    }
}
