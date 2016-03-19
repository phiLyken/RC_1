
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

class TileTools : EditorWindow
{
    
    TileManager Grid;
    PathFindingTest Pathing;
    TileManager main;
    bool debugTime = false;
    Tile selectedTile;

    [MenuItem("Foo/TileTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TileTools));
    }

    void GetObjects()
    {
        main = (TileManager)EditorGUILayout.ObjectField("Current Grid", main, typeof(TileManager), true, GUILayout.Width(position.width - 20), GUILayout.Height(20));

        if (main == null )
        {
            if(GUILayout.Button("Get Grid"))
            {
                main = FindObjectOfType<TileManager>();
            }
            return;
        }
        if (Grid == null || Grid != main.GetComponent<TileManager>()) 
        {
            Grid = main.GetComponent<TileManager>();
        }
        if (Pathing == null ||Pathing != main.GetComponent<PathFindingTest>() )
        {
            Pathing = main.GetComponent<PathFindingTest>();
        }

        if (Selection.activeGameObject != null)
        {
            selectedTile = Selection.activeGameObject.GetComponent<Tile>();
        }
    }
    void OnGUI()
    {

        GetObjects();
        DisplayPrefabOptions();
        SetPathingTile(selectedTile);      
        SelectTileTools(selectedTile);
        TileManagerTools();        
    }
    void DisplayPrefabOptions()
    { 

        if (main != null) {
            if (GUILayout.Button("Break Prefab"))
                 {
                PrefabUtility.DisconnectPrefabInstance(main);
            }

            if( GUILayout.Button("Save Prefab"))
            {
                PrefabUtility.ReplacePrefab(main.gameObject, PrefabUtility.GetPrefabParent(main.gameObject), ReplacePrefabOptions.ConnectToPrefab);
            }
        }
    }
    void TileManagerTools()
    {

        if (Grid == null) return;

        if (Selection.activeGameObject != null && 
            Selection.activeGameObject != Grid.gameObject && 
            Selection.activeGameObject.GetComponent<TileManager>() != null &&
            GUILayout.Button("Append Grid Test", GUILayout.Width(100))
            )
        {
            Grid.AppendGrid(Selection.activeGameObject.GetComponent<TileManager>());
        }


        if (GUILayout.Button("Spawn Random Region"))
        {
           
            Grid.AppendGrid(RegionLoader.GetRegion());
            SetVisualStateOnSelection("normal");
            if(CurrentTileSelection != null) { 
                CurrentTileSelection.Clear();
            }
        }


        if (GUILayout.Button(debugTime ? "Disable Crumble Debug" : "Enable Crumble Debug"))
        {

            foreach (Tile t in Grid.FetchTiles()) t.DebugCrumbleTime = !debugTime;
            debugTime = !debugTime;
        }


        if (GUILayout.Button("Select All"))
        {
            CurrentTileSelection.Clear();
            CurrentTileSelection.AddRange(Grid.GetTileList());
            SelectCurrentTilesInEditor();
            SetVisualStateOnSelection("selected");
        }
    }


    void SetPathingTile(Tile selectedTile)
    {

        if (Pathing == null) return;

        GUILayout.Label("Pathing", EditorStyles.whiteLargeLabel);

        if (Pathing != null && Pathing.endTile != null && Pathing.startTile != null && GUILayout.Button("Test Path", GUILayout.Width(100), GUILayout.Height(25)))
        {
            Pathing.CalculatePath();

        }

        if (GUILayout.Button("SetTile - Start", GUILayout.Width(100), GUILayout.Height(25)))
        {
            if (Pathing != null)
            {
                EditorUtility.SetDirty(Pathing);
            }

            Pathing.startTile = selectedTile;
        }

        if (GUILayout.Button("SetTile - End", GUILayout.Width(100), GUILayout.Height(25)))
        {
            if (Pathing != null)
            {
                EditorUtility.SetDirty(Pathing);
            }

            Pathing.endTile = selectedTile;

        }
    }

    void SelectTileTools(Tile selectedTile)
    {
        if (selectedTile == null) return;
        if (selectedTile != null)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select Row"))
            {
                CurrentTileSelection.AddRange(Grid.GetRow(selectedTile));
                SelectCurrentTilesInEditor();
                SetVisualStateOnSelection("selected");
            }

            if (GUILayout.Button("Select Col"))
            {
                CurrentTileSelection.AddRange(Grid.GetCol(selectedTile));
                SelectCurrentTilesInEditor();
                SetVisualStateOnSelection("selected");
            }

            if (GUILayout.Button("Select Same Height"))
            {
                CurrentTileSelection.AddRange(Grid.GetTilesAtHeight(selectedTile.currentHeightStep));
                SelectCurrentTilesInEditor();
                SetVisualStateOnSelection("selected");
            }

            EditorGUILayout.EndHorizontal();
        }
    }
    List<Tile> CurrentTileSelection;

    void OnSelectionChange()
    {
      //  Debug.Log("selected "+Selection.objects.Length);
        List<Tile> editorTiles = GetTilesInEditorSelection();

     //   Debug.Log("tiles selected:" + editorTiles.Count) ;

        //remove tiles that are not selected anymore
        if(CurrentTileSelection != null )
        {
            for(int i = CurrentTileSelection.Count-1; i >=0; i--)
            {
                if(editorTiles.Count == 0 || !editorTiles.Contains(CurrentTileSelection[i]))
                {
                    CurrentTileSelection[i].SetVisualState("normal");
                    CurrentTileSelection.RemoveAt(i);
                }
            }
        }
                
        //add tiles that are new
        if(editorTiles.Count > 0 )
        {
            if(CurrentTileSelection != null)
            {
                CurrentTileSelection = new List<Tile>();
                foreach(Tile t in editorTiles)
                {
                    if (!CurrentTileSelection.Contains(t))
                    {
                      //  Debug.Log("add tile to selected");
                        CurrentTileSelection.Add(t);
                    }
                }
            }
        }

        SetVisualStateOnSelection("selected");
    }

    void SelectCurrentTilesInEditor()
    {
        Object[] newSelection = new Object[CurrentTileSelection.Count];
        for (int i = 0; i < newSelection.Length; i++) newSelection[i] = CurrentTileSelection[i].gameObject;

        Selection.objects = newSelection;

    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
    void SetVisualStateOnSelection(string state)
    {
        if(CurrentTileSelection != null)
        {
            foreach (Tile t in CurrentTileSelection) t.SetVisualState(state);
        }
    }
  
    List<Tile> GetTilesInEditorSelection()
    {
        List<Tile> tiles = new List<Tile>();
        foreach (GameObject ob in Selection.gameObjects)
        {
                Tile t = (ob as GameObject).GetComponent<Tile>();
                if (t != null)
                {
                    tiles.Add(t);
                }
        }

        return tiles;
    }

    public void OnInspectorUpdate()
    {
        // This will only get called 10 times per second.
        Repaint();

    }
}