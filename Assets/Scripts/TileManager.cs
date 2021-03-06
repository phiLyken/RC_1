using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileManager : MonoBehaviour {

    public static List<TileGroup> Groups;

 
    public Tile[,] Tiles;

	public GameObject TilePrefab;

	public int RegionWidth;
	public int RegionHeight;


    [HideInInspector]
    public int GridWidth = 0;

    [HideInInspector]
    public int GridHeight = 0;

    float TileSize = 1;
    
    /// <summary>
    /// Returns the furthest unit of the specified owner id ( -1 for any owner)
    /// </summary>
    /// <param name="OwnerID"></param>
    /// <returns></returns>
    public Unit FindFirstUnit(int OwnerID)
    {
        int highestRow = 0;
        Unit highest = null;

        foreach (Unit u in Unit.AllUnits)
        {
             if (OwnerID == -1 || u.OwnerID == OwnerID) { 
                if (highest == null) highest = u;
                int curr_row = u.currentTile.TilePos.z;
                if (curr_row > highestRow)
                {
                    highestRow = curr_row;
                    highest = u;
                }
            }
        }

        return highest;
    }
    /// <summary>
    /// Returns the highest row of a player owned unit
    /// </summary>
    /// <returns></returns>
    public int FirstUnitRow(int Owner)
    {
        Unit first = FindFirstUnit(Owner);
        if(first != null)
            return first.currentTile.TilePos.z;

        return -1;
    }

    static TileManager _instance;

	public static TileManager Instance
    {
        get
        {
            if(_instance != null)
            {
                return _instance;
            } else
            {
                GameObject go = GameObject.FindGameObjectWithTag("Grid");
                if(go != null) { 
                    _instance = go.GetComponent<TileManager>();
                }

                if (_instance == null) { 
                   _instance = FindObjectOfType(typeof(TileManager)) as TileManager;
                }


                return _instance;
            }
        }
    }

	public Vector3 GridCenter{
		get{
			return Tiles[0,0].transform.position + new Vector3( (GridWidth * TileSize * 0.5f) - TileSize / 2, 0, GridHeight*TileSize * 0.5f);
		}
	}
	
    public Vector3 GetAppendPosition()
    {
        return transform.position + new Vector3( 0,0, GridHeight * TileSize);
    }

	public Tile GetClosestTileToInput(){
		return GetClosestTile( M_Math.GetPlaneIntersectionY(Camera.main.ScreenPointToRay( Input.mousePosition )));	
	}
	
	public Tile GetClosestTile(Vector3 pos){
		List<Tile> tiles = new List<Tile>();
     
		foreach(Tile t in Tiles){
			tiles.Add(t);
		}
		
		return GetClosestTile( tiles, pos);
	}

	public Tile GetClosestTile(List<Tile> _tiles, Vector3 pos){
		
		Tile bestTile = null;
      //  MDebug.Log(_tiles.Count);
		foreach(Tile t in _tiles){
			if(bestTile == null){
				bestTile = t;
				continue;
			}
			
			if( (pos - bestTile.transform.position).magnitude > (pos - t.transform.position).magnitude){
				bestTile = t;	
			}
		}

		return bestTile;
	} 

	public static List<Tile> FindPath(TileManager manager, Tile startTile, Tile endTile, Unit requester)
    {
        if(manager.Tiles == null) manager.SetTiles(manager.FetchTiles());
        return new TilePathFinder(manager, manager.GridWidth, manager.GridHeight).FindPath(startTile, endTile, requester);
    }

	public List<Tile> FindPath(Tile startTile, Tile endTile, Unit requester){
	
		return new TilePathFinder(Instance, Instance.GridWidth, Instance.GridHeight).FindPath(startTile, endTile, requester);	
	}
	
	void Awake () {
        
        if(_instance == null)
        {
            SpawnMeshes();
        }
        SetTiles(FetchTiles());

        MakeGroups();
        
    } 
    
	
    public List<Tile> GetTilesInRange(Tile center, float range)
    {
        List<Tile> tiles = new List<Tile>();
        
        foreach(Tile t in GetTileList())
        {
            if (GetTileDistance(center, t) <= range) tiles.Add(t);
        }

        return tiles;
    }
        
    float GetTileDistance(Tile t1, Tile t2)
    {
        return 
            Mathf.Max(
                Mathf.Abs(t1.TilePos.x - t2.TilePos.x), 
                Mathf.Abs(t1.TilePos.z - t2.TilePos.z)
            );

    }

	public List<Tile> GetTilesForFootPrint(Tile startTile, int Size){
		
		int x = startTile.TilePos.x;
		int z =  startTile.TilePos.z;
		
		List<Tile> tiles = new List<Tile>();
		
		if(x + Size <= GridWidth && z + Size <= GridHeight){
			for(int i = 0; i < Size; i++){
				for(int j = 0; j < Size; j++){
					tiles.Add( Tiles[ x + i, z+j ] );
				}
			}			
			return tiles;			
		}		
		return null;
	}
	
	public Vector3 GetCenterForFootPrint(Tile start, int size){
		return start.transform.position + new Vector3(TileSize * (size-1),0, TileSize * (size-1))/2;		
	}
	
    public List<TileGroup> MakeGroups()
    {
        
        if(Groups == null)
        {
            Groups = new List<TileGroup>();
        }
        Groups = TileGroup.GetGroupsFromTiles(GetTileList());

        return Groups;
         
    }
    public void SpawnBaseGrid()
    {
        _instance = this;
        EditorTileMeshContainer.Reset();
        GridHeight = 0;
        GridWidth = 0;
       
        int cc = transform.childCount;

        for (int i = cc - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
        AppendGrid(  SpawnTiles());
      
    }


    public void AppendGrid(TileManager manager)
    {
        float start = Time.realtimeSinceStartup;

        manager.transform.position = GetAppendPosition();
       

        Groups.AddRange(manager.MakeGroups());
        //MDebug.Log("[GROUPS] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));

        AppendGrid(manager.FetchTiles());

      //  MDebug.Log("[APPENDED] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));
       
    }

    public void SpawnMeshes()
    {

        GetTileList().ForEach(t => t.SpawnConfiguredMesh());
    }
    public void AppendGrid(Tile[,] newTiles)
    {
        float start = Time.realtimeSinceStartup;
        TilePos offset = new TilePos(0, GridHeight);

        AdjustGrid(newTiles.GetLength(0), newTiles.GetLength(1));
       //// MDebug.Log("[ADJUSTED] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));

        OffsetTilePositions(newTiles, offset);
      //  MDebug.Log("[OFFSET] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));
        SetTilesToGridPosition(newTiles);

       //// MDebug.Log("[TO POSITION] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));

        SetTiles(FetchTiles());
       // MDebug.Log("[SET TILES] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));

        SpawnMeshes();
       // MDebug.Log("[SPAWNED] APPEND _TIME = " + (Time.realtimeSinceStartup - start).ToString("0.000000000000"));

    }

    /// <summary>
    /// Offsets all "TilePos" coordinates by the offset
    /// </summary>
    /// <param name="tiles"></param>
    /// <param name="offset"></param>
    void OffsetTilePositions(Tile[,] tiles, TilePos offset)
    {
        foreach(Tile t in tiles)
        {

            t.TilePos.x += offset.x;
            t.TilePos.z += offset.z;
        }
    }


    Tile[,] SpawnTiles()
    {
        Tile[,] tiles = new Tile[RegionWidth, RegionHeight];

        for (int col = 0; col < RegionWidth; col++)
        {
            for (int row = 0; row < RegionHeight; row++)
            {
                Tile t = (
                         (GameObject)Instantiate(TilePrefab, Vector3.zero, Quaternion.identity)).GetComponent<Tile>();
                t.TilePos = new TilePos(col, row);
                t.transform.parent = this.transform;
                t.name = "tile_" + row + "_" + col;

                tiles[col, row] = t;

            }
        }

        return tiles;
    }


    void SetTiles(Tile[,] t)
    {
        Tiles = t;
    }
    
    /// <summary>
    /// Re-Definining the grid size (will empty Tiles array), should call "Fetch Tiles" afterwards)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void AdjustGrid(int x, int z)
    {
        int newWidth = x - GridWidth;
        GridWidth += newWidth;
        GridHeight += z;
        Tiles = new Tile[GridWidth, GridHeight];

       // MDebug.Log("Appending Grid: New Grid Size: " + GridWidth + "|" + GridHeight);
    }

    /// <summary>
    /// Get the height (actual coordninates) according to the tiles heighstep
    /// </summary>
    /// <param name="step"></param>
    /// <returns></returns>
    float GetTileHeight(int step)
    {
        return step * Constants.CrumbleDistancePerStep;
    }

    /// <summary>
    /// Get the world position of a tile according to the grid settings
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Vector3 GetTilePos2D(Tile t) 
    {
        return GetTilePos2D(t.TilePos.x, t.TilePos.z);
    }

    public Vector3 GetTilePos2D(int x, int y)
    {
        return new Vector3(transform.position.x + x * TileSize, 0, transform.position.z + y * TileSize);

    }

    public Vector3 GetTilePos(Tile t)
    {
        Vector3 position = GetTilePos2D(t);
        position.y = transform.position.y + GetTileHeight(t.currentHeightStep);
        return position;
    }
    /// <summary>
    /// Sets tiles to their horizontal position in the grid and makes them a child 
    /// </summary>
    public void SetTilesToGridPosition(Tile[,] tiles)
    {
        foreach ( Tile t in tiles)
        {
            Vector3 pos = GetTilePos(t);
            t.transform.parent = transform;
            t.transform.localPosition = pos;
        }
    }
    
    /// <summary>
    /// Returns a 2D Array of tiles that are childed to the gameobject (ordered by their tilepos)
    /// </summary>
    public Tile[,] FetchTiles()
    {
        float start = Time.realtimeSinceStartup;
       
        Tile[,] tiles = new Tile[GridWidth, GridHeight];
 
        Tile[] children = gameObject.GetComponentsInChildren<Tile>();

        foreach ( Tile t in children)
        {

         //   MDebug.Log(gameObject.name);
         //   MDebug.Log(gameObject.name + "_"+t.name);
            tiles[t.TilePos.x, t.TilePos.z] = t;
           
        }
       // MDebug.Log("[FETCH] " +  (Time.realtimeSinceStartup - start).ToString("0.0000000000000000000"));
        return tiles;
    }
    
	
	//Add Tiles top/right/bottom/left from the given tiles
	//Reduce range by one and repeat
	//Then all tiles around the start tiles should be selected	
	public List<Tile>  GetTilesInDistance(Vector3 center, float range){
		List<Tile> tilesInDistance = new List<Tile>();
		
		foreach ( Tile t in TileManager.Instance.Tiles){
			if( Vector3.Distance( center, t.transform.position) <= range	){
				tilesInDistance.Add(t);	
			}
		}
		
		return tilesInDistance;
	}
	
    /// <summary>
    /// Returns list of tiles that are surrounding the passed list of tiles
    /// </summary>
    /// <param name="tiles"></param>
    /// <param name="range"></param>
    /// <returns></returns>
	public static List<Tile> GetSurroundingTiles(List<Tile> tiles, int range, TileManager region){	
		
		if (range <= 0) return tiles;
		
		List<Tile> surrounding = new List<Tile>();
		List<Tile> additional = new List<Tile>();
		
//		MDebug.Log("Select tiles in range "+tiles.Count+"   r:"+range);
		
		
		foreach(Tile t in tiles){
			
			surrounding = GetSurroundingTiles(t, region);
			
			foreach(Tile s in surrounding){
				if( !tiles.Contains(s) && !additional.Contains(s)) additional.Add(s);
			}
		}
		tiles.AddRange(additional);
		
		range --;
		if(range > 0) {
			return GetSurroundingTiles(tiles, range, region);	
		} else {			
			return tiles;
		}
	}
	
    /// <summary>
    /// Returns List of tiles surrounding (vert, horiz, diagonal) around the passed tile
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
	public static List<Tile> GetSurroundingTiles( Tile t, TileManager region){
		List<Tile> surrounding = new List<Tile>();

        if (t == null) return surrounding;
		int x = t.TilePos.x;
		int z = t.TilePos.z;
		
		if(region.TileInField(x+1, z, region)){
			surrounding.Add(region.Tiles[x+1, z]);	
		}
		if(region.TileInField(x+1, z+1, region)){
			surrounding.Add(region.Tiles[x+1, z+1]);	
		}
		
		if(region.TileInField(x, z+1, region)){
			surrounding.Add(region.Tiles[x, z+1]);	
		}
		
		if(region.TileInField(x-1, z+1, region)){
			surrounding.Add(region.Tiles[x-1, z+1]);	
		}
		
		if(region.TileInField(x-1, z, region)){
			surrounding.Add(region.Tiles[x-1, z]);	
		}
		
		if(region.TileInField(x-1, z-1, region)){
			surrounding.Add(region.Tiles[x-1, z-1]);	
		}
		
		if(region.TileInField(x, z-1, region)){
			surrounding.Add(region.Tiles[x, z-1]);	
		}
		
		if(region.TileInField(x+1, z-1, region)){
			surrounding.Add(region.Tiles[x+1, z-1]);	
		}
		//		MDebug.Log(surrounding.Count);
		
		return surrounding;
		
	}

    /// <summary>
    /// Find the tiles that have a neighbour that doesnt belong to the area
    /// </summary>
    /// <param name="area"></param>
    /// <param name="region"></param>
    /// <returns></returns>
    public static List<Tile> GetBorderTiles(List<Tile> area, TileManager region, bool exclude_inaccessible)
    {
        List<Tile> tiles = new List<Tile>();
        
        foreach(Tile t in area)
        {
            foreach(Tile t2 in  GetSurroundingTiles(t, region))
            {
                if (t2.isAccessible && !area.Contains(t2))
                {
                  
                             tiles.Add(t);
                    continue;
                }
            }
        }

        return tiles;
    }

    /// <summary>
    /// checks whether the passed tile is within the field
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
	bool TileInField(int x, int y, TileManager region){
		return x >= 0 && y >= 0 &&
			x < region.GridWidth && y < region.GridHeight;
	}	

	
	public List<Tile> GetEdgeTiles( List<Tile> tiles, int range, TileManager region){
		List<Tile> baseTiles = new List<Tile>(tiles);
		List<Tile> all = GetSurroundingTiles(baseTiles, range, region);		
		
		foreach(Tile t in all) Debug.DrawRay(t.transform.position, Vector3.up, Color.grey, 10);
		
		for(int i = 0; i < tiles.Count; i++){
			all.Remove(tiles[i]);
		}			
		return all;
		
	}
	public void InvalidateGroups()
    {
        Groups = null;
    }
	void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetAppendPosition(), 0.5f);

         Color[] col =
         {
            Color.red, Color.cyan, Color.yellow, Color.blue, Color.green
            };


        
        if (Groups != null) {

            int index = 0;

            foreach(TileGroup current_group in Groups)
            {
                index++;

                Gizmos.color = col[index % (col.Length - 1)];

                if(current_group.Group.Count > 1) { 
                    foreach (Tile t in current_group.Group)
                    {
                       if(t != null)
                        Gizmos.DrawWireCube(t.transform.position, new Vector3(t.transform.localScale.x, 0.1f, t.transform.localScale.z));
                    }
                }
            }
        } else
        {
            Groups = TileGroup.GetGroupsFromTiles(GetTileList());
        }
    }
	
    public List<Tile> GetRow(int rownum)
    {
        List<Tile> tiles = new List<Tile>();

        rownum = Mathf.Clamp(rownum, 0, GridHeight);
        if (Tiles == null)
            Tiles = FetchTiles();

        for (int i = 0; i < GridWidth; i++)
        {
            tiles.Add(Tiles[i, rownum]);
        }

        return tiles;
    }
    public List<Tile> GetRow(Tile t)
    {

        return GetRow(t.TilePos.z);
    }
    public List<Tile> GetTilesAtHeight(int height)
    {
        List<Tile> tiles = new List<Tile>();

        if (Tiles == null) Tiles = FetchTiles();

        foreach (Tile t in Tiles)
        {
            if (t.currentHeightStep == height) tiles.Add(t);
        }

        return tiles;
    }

    public List<Tile> GetCol(Tile t)
    {
        int colNum = t.TilePos.x;
        List<Tile> tiles = new List<Tile>();

        if (Tiles == null) Tiles = FetchTiles();

        for (int i = 0; i < GridHeight; i++)
        {
            tiles.Add(Tiles[colNum, i]);
        }

        return tiles;
    }

    public List<Tile> GetRandomTilesAroundCenter(Tile center, float distance)
    {
       return  M_Math.GetObjectsInRangeAroundCenter(center, GetTileList().Cast<MonoBehaviour>().ToList(), distance).Cast<Tile>().ToList();
      
    }




    public List<Tile> GetTileList()
    {
        if (Tiles == null )
        {
           Tiles = FetchTiles();
        }
       
        List<Tile> l = new List<Tile>();

        for(int i = 0; i < Tiles.GetLength(0); i++)
        {
            for(int j = 0; j < Tiles.GetLength(1); j++)
            {
                l.Add(Tiles[i, j]);
            }
        }

        return l;
    }

	/// <summary>
	/// gets the first row# that has at least 1 active tile
	/// </summary>
	/// <returns>The last active row.</returns>
	public int GetLastActiveRow(){
		for(int i = 0 ; i < GridHeight; i++){
			for(int j = 0; j < GridWidth; j++){
				if(Tiles[j,i].isAccessible && !Tiles[j,i].isCrumbling) return i;
			}
		}
		return -1;
	}

    public Tile GetFirstNotCrumblingTileInCol(int column)
    {
        if (Tiles == null)
        {
            Tiles = FetchTiles();
        }
        for (int i = 0; i < Tiles.GetLength(1); i++)
        {
            Tile current = Tiles[column, i];
            if(current.CrumbleStage == 0)
            {
                return current;
            }
        }
        Debug.LogWarning("No not-crumbling-tile found (column " + column + ")");
        return null;
    }

    public Tile GetTileClamped(int x, int y)
    {
        return Tiles[ Mathf.Clamp(x, 0, Tiles.GetLength(0)-1), Mathf.Clamp(Mathf.Max(0,y), GetLastActiveRow(), Tiles.GetLength(1)-1)];  
    }
 }
