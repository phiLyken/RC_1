using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileEditor : Editor {

	Tile m_tile;

	void OnEnable () {
		m_tile = target as Tile;
	}

    int selected = 0;
    //  Tile m_tile;


    public override void OnInspectorGUI()
    {
        ToggleInspector();
        GUILayout.Space(15);
        CustomProperties();
        GUILayout.Space(15);
        MoveTiles();
        GUILayout.Space(15);
        CrumbleTiles();
        GUILayout.Space(15);
        MiscTools();

        /*
          int newSelected = EditorGUILayout.Popup("Label", selected, LoadTileAssets());
        if (GUILayout.Button("Spawn"))
        {
            foreach (Tile t in targets) SpawnSelected(t);
            SceneView.RepaintAll();
        }

         if (newSelected != selected)
        {      
            selected = newSelected;
        }
        */

    }


    bool showInspector;

    void ToggleInspector()
    {
        showInspector = EditorGUILayout.Foldout(showInspector, "SHOW DEFAULT INSPECTOR");
        if (showInspector)
            base.OnInspectorGUI();
    }

    void CustomProperties()
    {
        EditorGUILayout.BeginHorizontal();


        if (GUILayout.Button("Toggle Blocked"))
        {
            foreach (Tile t in targets)
            {

                t.ToggleBlocked();
                SceneView.RepaintAll();
            }
        }


        if (GUILayout.Button("Toggle Camp"))
        {
            foreach (Tile t in targets)
            {
                t.ToggleCamp();
                SceneView.RepaintAll();
            }
        }

        if (GUILayout.Button("Toggle BlockSight"))
        {
            foreach (Tile t in targets)
            {
                t.ToggleBlockSight();
                SceneView.RepaintAll();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (targets.Length > 1)
        {
            if (GUILayout.Button("Reset Custom Settings"))
            {
                foreach (Tile t in targets)
                {
                    BakeGrid.ResetTile(t);

                    SceneView.RepaintAll();
                }
            }

            EditorGUILayout.HelpBox(".. not supported in multiselect", MessageType.Info, true);
        }
        else if ((target as Tile).customTile)
        {

            if (GUILayout.Button("Reset Custom Settings"))
            {
				BakeGrid.ResetTile(target as Tile);
            }
            EditorGUILayout.HelpBox("CUSTOM SETTINGS! \nCustom tile settings will not get properties from props", MessageType.Warning, true);
            SceneView.RepaintAll();
        }

    }

    void MoveTiles()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Move Up"))
        {
            foreach (Tile t in targets)
            {
                t.MoveTileUp(1);
                SceneView.RepaintAll();
            }
        }

        if (GUILayout.Button("Move Down"))
        {
            foreach (Tile t in targets)
            {
                t.MoveTileDown(1);
                SceneView.RepaintAll();
            }
        }


        EditorGUILayout.EndHorizontal();
    }

    void CrumbleTiles()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Force Crumble"))
        {
            foreach (Tile t in targets)
            {
                t.StartCrumble();
                t.OnCrumbleTurn(0);
                SceneView.RepaintAll();
            }
        }


        if (GUILayout.Button("Reset Crumble"))
        {
            foreach (Tile t in targets)
            {
                t.ResetCrumble();
                SceneView.RepaintAll();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void MiscTools()
    {
        if (GUILayout.Button("Toggle Enemy Spawn"))
        {
            foreach (Tile t in targets)
            {
                MakeEnemySpawnTile(t);
            }
        }


        if (GUILayout.Button("Add/Remove Loot"))
        {
            foreach (Tile t in targets)
            {
                ToggleLoot(t);
            }
        }

        if (GUILayout.Button("Spawn Mesh"))
        {
            foreach (Tile t in targets)
            {
                SpawnMesh(t);
            }
        }
    }

    public static void SpawnMesh(Tile t)
    {
        TileMesh new_mesh = t.SpawnConfiguredMesh().GetComponent<TileMesh>();
        
    }
  
    void ToggleLoot(Tile t)
    {
        Tile_Loot l = t.GetComponent<Tile_Loot>();
        if (l == null)
        {
            l = t.gameObject.AddComponent<Tile_Loot>();
            GameObject lootobj = (Instantiate(Resources.Load("Loot"), t.GetPosition(), Quaternion.identity) as GameObject);
            lootobj.transform.SetParent(t.transform, true);
            l.LootObject = lootobj;
        }
        else
        {
            DestroyImmediate(l.LootObject.gameObject);
            DestroyImmediate(l);
        }
    }
    void MakeEnemySpawnTile(Tile t)
    {
        
        UnitSpawnManager manager = t.transform.parent.GetComponent<UnitSpawnManager>();
        if(manager == null)
        {
            Debug.LogWarning("No UnitSpawn Manager Found in parent of this tile");

        }
        UnitSpawner s = t.GetComponent<UnitSpawner>();
        if (s == null)
        {
            s = t.gameObject.AddComponent<UnitSpawner>();
        } else
        {
            DestroyImmediate(s);
        }
       

        EditorUtility.SetDirty(target);

    }


    void SpawnSelected(Tile t)
    {
        t.SpawnConfiguredMesh();
    }

    string[] LoadTileAssets()
    {
        Object[] assets = Resources.LoadAll("tiles/meshes");
        string[] object_names = new string[assets.Length];

        for (int i = 0; i < object_names.Length; i++)
        {
            object_names[i] = (assets[i] as GameObject).name;
        }

        return object_names;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube((target as Tile).transform.position, Vector3.one);
    }
    

}
