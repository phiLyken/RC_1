using UnityEngine;
using System.Collections;

public class AddLootOnSpawn : MonoBehaviour {

    public LootCategory Category;

  

	void Start () {

       // MDebug.Log("add for " + Category);
        Tile_Loot.AddLoot(this.GetComponent<Tile>(), Category);
        Destroy(this);
	}

    void OnDrawGizmos()
    {
        Color[] c = { Color.blue, Color.yellow, Color.magenta };
   
        Gizmos.color = c[(int)Category % c.Length];

        Gizmos.DrawWireSphere(transform.position + Vector3.up * 1, 0.5f);
    }



}
