using UnityEngine;
using System.Collections;
using System.Linq;

public class UnitSpawner : MonoBehaviour {

    public bool SpawnOnAwake;

    public string[] Unit_IDs;

    void Awake()
    {
        if (SpawnOnAwake) {
            SpawnUnit();
        }
    }
    public void SpawnUnit()
    {
        Unit u = UnitFactory.CreateUnit(UnitConfigsDatabase.GetConfig(MyMath.GetRandomObject(Unit_IDs.ToList())));
        UnitFactory.SpawnUnit(u, GetComponent<Tile>());
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position , Vector3.one);

        MyMath.SceneViewText(MyMath.StringArrToLines(Unit_IDs), transform.position + Vector3.up, Color.magenta);
    }
}
