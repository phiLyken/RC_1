using UnityEngine;
using System.Collections;

public class UnitSpawner : MonoBehaviour {

    public string unitID;

    void Awake()
    {
        Unit u = UnitSpawnManager.CreateUnit(UnitConfigsDatabase.GetConfig(unitID));
        u.SetTile(GetComponent<Tile>(), true);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.up, Vector3.one * 0.5f);
       
    }
}
