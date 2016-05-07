using UnityEngine;
using System.Collections;
using System.Linq;

public class UnitSpawner : MonoBehaviour {

    public bool SpawnOnAwake;

    public string[] Unit_Ids;
    public int group;
    public MyMath.R_Range TurnTimeOnSpawn;

    Color[] group_color =
    {
        Color.grey,
        Color.blue,
        Color.cyan,
        Color.green,
        Color.yellow
    };

    void Awake()
    {
        if (SpawnOnAwake) {
            SpawnUnit();
        }
    }
    public void SpawnUnit()
    {
        Unit u = UnitFactory.CreateUnit(UnitConfigsDatabase.GetConfig(MyMath.GetRandomObject(Unit_Ids.ToList())), group, (int) TurnTimeOnSpawn.Value());

        if(u != null)
            UnitFactory.SpawnUnit(u, GetComponent<Tile>());
    }
    void OnDrawGizmos()
    {
        if (SpawnOnAwake) { 
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one*0.25f);
        }

        Gizmos.color = group_color[Mathf.Clamp(group, 0, group_color.Length - 1)] ;
        Gizmos.DrawWireCube(transform.position , Vector3.one);

        MyMath.SceneViewText(MyMath.StringArrToLines(Unit_Ids), transform.position + Vector3.up, Color.magenta);
    }
}
