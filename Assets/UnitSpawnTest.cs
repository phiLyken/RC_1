using UnityEngine;
using System.Collections;

public class UnitSpawnTest : MonoBehaviour {

    public UnitConfig Config;

    void Start()
    {
        Unit u =  UnitSpawnManager.CreateUnit(Config);

        u.SetTile(GetComponent<Tile>(), true);
    }
}
