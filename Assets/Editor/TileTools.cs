
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class TileTools : EditorWindow
{
    List<Tile> CurrentTileSelection;
    TileManager Grid;
    PathFindingTest Pathing;
    TileManager main;
    bool debugTime = false;
    Tile selectedTile;
    List<TileWeighted> weighted;

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
        if (Pathing == null || Pathing != main.GetComponent<PathFindingTest>() )
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
           WorldExtender.SpawnRegion(  RegionLoader.GetWeightedRegionForLevel(0), Grid);
          
            if(CurrentTileSelection != null) { 
                CurrentTileSelection.Clear();
            }
        }

        if (GUILayout.Button("Spawn Next Region"))
        {
            WorldExtender.Instance.SpawnNext();

            if (CurrentTileSelection != null)
            {
                CurrentTileSelection.Clear();
            }
        }

        if (GUILayout.Button("Spawn Camp Region"))
        {

            WorldExtender.SpawnRegion(RegionLoader.GetCamp(0), Grid);
         
            if (CurrentTileSelection != null)
            {
                CurrentTileSelection.Clear();
            }
        }

        if (GUILayout.Button("Select All"))
        {
            CurrentTileSelection.Clear();
            CurrentTileSelection.AddRange(Grid.GetTileList());
            SelectCurrentTilesInEditor();
           
        }
        if (GUILayout.Button("Select Border"))
        {
            List<Tile> border = TileManager.FindBorderTiles(CurrentTileSelection, Grid, true);
            CurrentTileSelection.Clear();
            CurrentTileSelection.AddRange(border);
            SelectCurrentTilesInEditor();

        }

        if (GUILayout.Button("Show Current Crumble Weights"))
        {
            if(weighted == null) { 
                weighted = TileWeighted.GetWeightedTiles(Grid);
            } else
            {
                weighted = null;
            }
            SceneView.RepaintAll();
          
        }

        if (GUILayout.Button("Reset Tile Visual States"))
        {

            foreach (Tile t in Grid.FetchTiles())
            {          
              
                t.SetBaseState();
            }
        }

        if (Application.isPlaying && GUILayout.Button("Test Crumble"))
        {
   
            TileWeighted.GetCrumbleTiles(20,Grid).ForEach(t =>t.StartCrumble());

            //  Debug.Log(Grid.GetLastActiveRow());
           
            Grid.GetTileList().ForEach(t => t.OnCrumbleTurn(0));
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Spawn_Meshes"))
        {

            List<Tile> tiles = Grid.GetTileList();
            foreach(Tile t in tiles)
            {
                TileEditor.SpawnMesh(t);
            }
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Bake"))
        {
            BakeGrid.Bake(Grid);
            SceneView.RepaintAll();
        }
    }


    void SetPathingTile(Tile selectedTile)
    {

        if (Pathing == null) return;

        GUILayout.Label("Pathing", EditorStyles.whiteLargeLabel);

        if (Pathing != null && Pathing.endTile != null && Pathing.startTile != null && GUILayout.Button("Test Path", GUILayout.Width(100), GUILayout.Height(25)))
        {
            Pathing.CalculatePath();
            SceneView.RepaintAll();
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
            if (CurrentTileSelection == null) CurrentTileSelection = new List<Tile>();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select Row"))
            {

                Debug.Log(Grid.GetRow(selectedTile));

                CurrentTileSelection.AddRange(Grid.GetRow(selectedTile));
                SelectCurrentTilesInEditor();               
            }

            if (GUILayout.Button("Select Col"))
            {
                CurrentTileSelection.AddRange(Grid.GetCol(selectedTile));
                SelectCurrentTilesInEditor();               
            }


            if (GUILayout.Button("Select Same Height"))
            {
                CurrentTileSelection.AddRange(Grid.GetTilesAtHeight(selectedTile.currentHeightStep));
                SelectCurrentTilesInEditor();              
            }


            /*
            if (GUILayout.Button("Select Region"))
            {
                CurrentTileSelection.AddRange(Grid.GetRegion(selectedTile, Grid));
                SelectCurrentTilesInEditor();
                SetVisualStateOnSelection("selected");
            }
            */
            EditorGUILayout.EndHorizontal();
        }
    }

    
    void OnSelectionChange()
    {
        //Debug.Log("selected "+Selection.objects.Length);
        List<Tile> editorTiles = GetTilesInEditorSelection();

        //Debug.Log("tiles selected:" + editorTiles.Count) ;
       

        //remove tiles that are not selected anymore
        if (CurrentTileSelection != null )
        {          

            for (int i = CurrentTileSelection.Count-1; i >=0; i--)
            {
                if(editorTiles.Count == 0 || !editorTiles.Contains(CurrentTileSelection[i]))
                {                   
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
    }

    void SelectCurrentTilesInEditor()
    {
        Selection.objects = (from t in CurrentTileSelection.Where(t => t != null) select t.gameObject).ToArray();
    }

  
    List<Tile> GetTilesInEditorSelection()
    {
        return (from ob in Selection.gameObjects where ob.GetComponent<Tile>() != null select ob.GetComponent<Tile>()).ToList();
    }

    public void OnInspectorUpdate()
    {
        // This will only get called 10 times per second.
        Repaint();
    }    

    // Window has been selected
    void OnFocus()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        // Add (or re-add) the delegate.
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDestroy()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }


    void OnSceneGUI(SceneView sceneView)
    {
        // Do your drawing here using Handles.
        Handles.BeginGUI();
       
        if (weighted != null && Grid != null)
        {
            float sum_weights = weighted.Select(t => t.weight).Sum();
          
            foreach (TileWeighted tw in weighted) {
                float percent = (tw.weight / sum_weights)*100;
                if(percent > 0)
                MyMath.SceneViewText(percent.ToString("00.00") + "%", Grid.Tiles[tw.tilePos.x, tw.tilePos.z].GetPosition(), Color.black);
            }
        }
        Handles.EndGUI();
    }
}