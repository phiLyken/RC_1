using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFindingTest : MonoBehaviour {

   
	List<Tile> CurrentPath = new List<Tile>();

    public Tile startTile;
    public Tile endTile;

    public int foo;

	void ClearCurrentPath(){

		CurrentPath.Clear();
	}
	
    public void CalculatePath()
    { 
      //  Debug.Log("calc path");
        if( startTile != null && endTile !=null)
        {
            CurrentPath = TileManager.FindPath(GetComponent<TileManager>(),startTile, endTile);

          //  Debug.Log("waypoints " + CurrentPath.Count);
            foo = CurrentPath.Count; 
        }
    }
	


    public void OnDrawGizmos()
    {
        if(endTile != null) { 
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(endTile.transform.position, Vector3.one * 0.2f);
        }

        if (startTile != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(startTile.transform.position, Vector3.one * 0.2f);
        }
        Gizmos.color = Color.blue;
        if (CurrentPath == null || CurrentPath.Count == 0 ||CurrentPath[0] == null) return;

        Vector3 f = CurrentPath[0].transform.position;

        foreach (Tile t in CurrentPath)
        {
           
            Debug.DrawLine(f + Vector3.up * 0.5f, t.transform.position + Vector3.up * 0.5f, Color.blue);
            f = t.transform.position;
          
            Gizmos.DrawWireSphere(t.transform.position + Vector3.up * 0.5f, 0.15f);
        }
    }
}
