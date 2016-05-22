using UnityEngine;
using System.Collections;
using System.Linq;

public class UnitSpawner : MonoBehaviour {

    public int group;
    

    Color[] group_color =
    {
        Color.grey,
        Color.blue,
        Color.cyan,
        Color.green,
        Color.yellow
    };


    public void SpawnUnit(ScriptableUnitConfig unit_config, int turnTime)
    {
        Unit u = UnitFactory.CreateUnit(unit_config, group, turnTime);
       // Debug.Log(gameObject.name+" spawning " + u.GetID());
      

        if(u != null) { 
            UnitFactory.SpawnUnit(u, GetComponent<Tile>());
        } else
        {
            Debug.LogWarning("Could not spawn unit");
        }
    }
    void OnDrawGizmos()
    {

        Gizmos.color = group_color[Mathf.Clamp(group, 0, group_color.Length - 1)] ;
        Gizmos.DrawWireCube(transform.position , Vector3.one);

        //MyMath.SceneViewText(MyMath.StringArrToLines(Unit_Ids), transform.position + Vector3.up, Color.magenta);
    }
}
