using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileEditor : Editor {

   
    int selected = 0;
    Tile m_tile;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_tile = (target as Tile);
        if (GUILayout.Button("Toggle Blocked"))
        {
            foreach (Tile t in targets)
            {
                t.ToggleBlocked();
            }
        }
        if (GUILayout.Button("Up"))
        {
            foreach (Tile t in targets)
            {
                t.MoveTileUp();
            }
        }
        
        if (GUILayout.Button("Down"))
        {
            foreach (Tile t in targets)
            {
                t.MoveTileDown();
            }
        }

        int newSelected = EditorGUILayout.Popup("Label", selected, LoadTileAssets());

        if (GUILayout.Button("Spawn"))
        {
            foreach (Tile t in targets) SpawnSelected(t);
        }
        if (newSelected != selected)
        {      
            selected = newSelected;
        }
       
    }

    void SpawnSelected(Tile t)
    {
        t.SetMesh((GameObject)Instantiate(Resources.Load("tiles/" + LoadTileAssets()[selected])));
    }

    string[] LoadTileAssets()
    {
        Object[] assets = Resources.LoadAll("tiles");
        string[] object_names = new string[assets.Length];

        for (int i = 0; i < object_names.Length; i++)
        {
            object_names[i] = (assets[i] as GameObject).name;
        }

        return object_names;
    }

    

}
