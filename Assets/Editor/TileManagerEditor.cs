using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(TileManager))]
[CanEditMultipleObjects]
public class TileManagerEditor : Editor {


    void OnEnable()
    {
       

    }
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		
		if( GUILayout.Button("Reset Grid")) (target as TileManager).SpawnBaseGrid();
       // if (GUILayout.Button("Fetch Tiles Grid")) (target as TileManager).FetchTiles();
        if (GUILayout.Button("Update Positions")) (target as TileManager).SetTilesToGridPosition( (target as TileManager).FetchTiles());
    }

}
