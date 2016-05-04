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
        base.OnInspectorGUI();
      //  m_tile = (target as Tile);
        if (GUILayout.Button("Toggle Blocked"))
        {
            foreach (Tile t in targets)
            {
                t.ToggleBlocked();
            }
        }
        if (GUILayout.Button("Toggle Camp"))
        {
            foreach (Tile t in targets)
            {
                t.ToggleCamp();
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
                Tile_Loot l = t.GetComponent<Tile_Loot>();
                if(l == null)
                {
                    l = t.gameObject.AddComponent<Tile_Loot>();
                    GameObject lootobj = (Instantiate(Resources.Load("Loot"), t.GetPosition(), Quaternion.identity) as GameObject );
                    lootobj.transform.SetParent(t.transform, true);
                    l.LootObject = lootobj;
                } else
                {
                    DestroyImmediate(l.LootObject.gameObject);
                    DestroyImmediate(l);
                }
            }
        }


        if (newSelected != selected)
        {      
            selected = newSelected;
        }
       
    }

    void MakeEnemySpawnTile(Tile t)
    {
        Debug.Log("asds");
        UnitSpawnManager manager = t.transform.parent.GetComponent<UnitSpawnManager>();
        if(manager == null)
        {
            Debug.LogWarning("No UnitSpawn Manager Found in parent of this tile");

        }
        if (!manager.SpawnTiles.Contains(t))
        {
            Debug.LogWarning("Tile added");
            manager.SpawnTiles.Add(t);
        } else
        {
            manager.SpawnTiles.Remove(t);
            Debug.LogWarning("Tile removed");
        }

        EditorUtility.SetDirty(target);

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
