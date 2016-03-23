using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class TileManager : MonoBehaviour {
    
    public static float HeighSteps = 0.5f;
    public Tile[,] Tiles;

	public GameObject TilePrefab;


	public int RegionWidth;
	public int RegionHeight;


    [HideInInspector]
    public int GridWidth = 0;

    [HideInInspector]
    public int GridHeight = 0;

    float TileSize = 1;

   


	public static TileManager Instance;

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
		return GetClosestTile( MyMath.GetPlaneIntersectionY(Camera.main.ScreenPointToRay( Input.mousePosition )));	
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
      //  Debug.Log(_tiles.Count);
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

    public static List<Tile> FindPath(TileManager manager, Tile startTile, Tile endTile)
    {
        if(manager.Tiles == null) manager.SetTiles(manager.FetchTiles());
        return new TilePathFinder(manager, manager.GridWidth, manager.GridHeight).FindPath(startTile, endTile);
    }

    public List<Tile> FindPath(Tile startTile, Tile endTile){
	
		return new TilePathFinder(Instance, Instance.GridWidth, Instance.GridHeight).FindPath(startTile, endTile);	
	}
	
	void Awake () {
        SetTiles(FetchTiles());

        if(gameObject.tag == "Grid")
         Instance = this;       

	} 

	public bool AreTilesAdjacent(Tile t1, Tile t2){
		foreach (Tile t in t1.AdjacentTiles){
			if(t2.AdjacentTiles.Contains(t)) return true;	
		}
		
		return false;
	}
	
    public List<Tile> GetTilesInRange(Tile center, int range)
    {
        List<Tile> tiles = new List<Tile>();
        foreach(Tile t in Tiles)
        {
            if (GetTileDistance(center, t) <= range) tiles.Add(t);
        }

        return tiles;
    }

   
    public List<Tile> GetReachableTiles(Tile from, List<Tile> tiles, Unit unit)
    {      
        List<Tile> reacheable = new List<Tile>();      
        foreach (Tile t in tiles)
        {
            List<Tile> path = FindPath(from, t);           
            if (unit.PathWalkable(path)) reacheable.Add(t);
        }

        return reacheable;
    }

    
    float GetTileDistance(Tile t1, Tile t2)
    {
        return 
            Mathf.Max(
                Mathf.Abs(t1.TilePos.x - t2.TilePos.x), 
                Mathf.Abs(t1.TilePos.z - t2.TilePos.z)
            );

    }
    float GetDistance2D(Vector3 v1, Vector3 v2)
    {
        v1.y = 0;
        v2.y = 0;

        return (v1 - v2).magnitude;
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
	

    public void SpawnBaseGrid()
    {
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

        AppendGrid(manager.FetchTiles());
        DestroyImmediate(manager.gameObject);
       
    }
    public void AppendGrid(Tile[,] newTiles)
    {
        TilePos offset = new TilePos(0, GridHeight);

        AdjustGrid(newTiles.GetLength(0), newTiles.GetLength(1));
        OffsetTilePositions(newTiles, offset);
        SetTilePhaseID(newTiles, WorldExtender.currentPhaseID);
        SetTilesPos(newTiles);       
        SetTiles(FetchTiles());

    }
    void SetTilePhaseID(Tile[,] tiles, int id)
    {
        foreach (Tile t in tiles) t.zoneID = id;
    }
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
    
    public void AdjustGrid(int x, int z)
    {
        int newWidth = x - GridWidth;
        GridWidth += newWidth;
        GridHeight += z;
        Tiles = new Tile[GridWidth, GridHeight];

        Debug.Log("Appending Grid: New Grid Size: " + GridWidth + "|" + GridHeight);
    }


    float GetTileHeight(int step)
    {
        return step * HeighSteps;
    }

    public Vector3 GetTilePos2D(int x, int y)
    {
       return new Vector3(x * TileSize, 0, y * TileSize)
         + transform.position + new Vector3(TileSize * 0.5f, 0, TileSize * 0.5f);
    }

    
    /// <summary>
    /// Sets tiles to their horizontal position in the grid and makes them a child 
    /// </summary>
    public void SetTilesPos(Tile[,] tiles)
    {

        foreach ( Tile t in tiles)
        {

            Vector3 pos = GetTilePos2D(t.TilePos.x, t.TilePos.z);
            t.transform.parent = transform;
            t.transform.position = new Vector3(pos.x, GetTileHeight(t.currentHeightStep), pos.z);
        }
    }
    
    /// <summary>
    /// Returns a list of tiles that are attached to the gameobject (ordered by their tilepos)
    /// </summary>
    public Tile[,] FetchTiles()
    {
        Tile[,] tiles = new Tile[GridWidth, GridHeight];
 
        Tile[] children = gameObject.GetComponentsInChildren<Tile>();

        foreach ( Tile t in children)
        {

         //   Debug.Log(gameObject.name);
         //   Debug.Log(gameObject.name + "_"+t.name);
            tiles[t.TilePos.x, t.TilePos.z] = t;
            t.Manager = this;
        }

        return tiles;
    }

	
	List<Tile> SelectAdjactentTiles(Tile center, int range, int currentRange){
		
		List<Tile> AdjTiles = new List<Tile>();
		int row = center.TilePos.x;
		int col =  center.TilePos.z;
		
		
		//LEFT+RIGHT
				//attemptLeft
		if(col-1 >= 0){
			AdjTiles.Add(Tiles[row,col-1]);
			if(currentRange<range) SelectAdjactentTiles(Tiles[row,col-1], range, currentRange+1);
		}		
		//attemptRight
		if(col+1 < GridWidth){
			AdjTiles.Add(Tiles[row,col+1]);
		}
		
		
		//TOP
		if(row+1 < GridHeight){
            AdjTiles.Add(Tiles[row+1,col]);	
		}
			
		//Bottom
		if(row-1 >= 0){
            AdjTiles.Add(Tiles[row-1,col]);	
		}
	
		return AdjTiles;
	}
	

	
	//Add Tiles top/right/bottom/left from the given tiles
	//Reduce range by one and repeat
	//Then all tiles around the start tiles should be selected	
	public List<Tile>  GetTilesInDistance(Vector3 center, float range){
		List<Tile> tilesInDistance = new List<Tile>();
		
		foreach ( Tile t in TileManager.Instance.Tiles){
			if( Vector3.Distance( center, t.transform.position) < range	){
				tilesInDistance.Add(t);	
			}
		}
		
		return tilesInDistance;
	}
	
	public List<Tile> SelectTilesInRange(List<Tile> tiles, int range){	
		
		if (range <= 0) return tiles;
		
		List<Tile> surrounding = new List<Tile>();
		List<Tile> additional = new List<Tile>();
		
//		Debug.Log("Select tiles in range "+tiles.Count+"   r:"+range);
		
		
		foreach(Tile t in tiles){
			
			surrounding = GetSurroundingTiles(t);
			
			foreach(Tile s in surrounding){
				if( !tiles.Contains(s) && !additional.Contains(s)) additional.Add(s);
			}
		}
		tiles.AddRange(additional);
		
		range --;
		if(range > 0) {
			return SelectTilesInRange(tiles, range);	
		} else {			
			return tiles;
		}
	}
	
	public List<Tile> GetSurroundingTiles( Tile t){
		List<Tile> surrounding = new List<Tile>();	
		
		int x = t.TilePos.x;
		int z = t.TilePos.z;
		
		if(TileInField(x+1, z)){
			surrounding.Add( TileManager.Instance.Tiles[x+1, z]);	
		}
		if(TileInField(x+1, z+1)){
			surrounding.Add(TileManager.Instance.Tiles[x+1, z+1]);	
		}
		
		if(TileInField(x, z+1)){
			surrounding.Add( TileManager.Instance.Tiles[x, z+1]);	
		}
		
		if(TileInField(x-1, z+1)){
			surrounding.Add( TileManager.Instance.Tiles[x-1, z+1]);	
		}
		
		if(TileInField(x-1, z)){
			surrounding.Add( TileManager.Instance.Tiles[x-1, z]);	
		}
		
		if(TileInField(x-1, z-1)){
			surrounding.Add( TileManager.Instance.Tiles[x-1, z-1]);	
		}
		
		if(TileInField(x, z-1)){
			surrounding.Add( TileManager.Instance.Tiles[x, z-1]);	
		}
		
		if(TileInField(x+1, z-1)){
			surrounding.Add( TileManager.Instance.Tiles[x+1, z-1]);	
		}
		//		Debug.Log(surrounding.Count);
		
		return surrounding;
		
	}
	bool ValidTile(int row, int col, List<Tile> tiles){
		return TileInField(row, col) && !tiles.Contains( TileManager.Instance.Tiles[row,col]);  
	}
	
	bool TileInField(int x, int y){
		return x >= 0 && y >= 0 &&
			x < TileManager.Instance.GridWidth && y < TileManager.Instance.GridHeight;
	}
	
	
    public List<Tile> FindReachableTiles(Tile start, int range)
    {
        return null;
    }
	
	public List<Tile> GetEdgeTiles( List<Tile> tiles, int range){
		List<Tile> baseTiles = new List<Tile>(tiles);
		List<Tile> all = SelectTilesInRange(baseTiles, range);		
		
		foreach(Tile t in all) Debug.DrawRay(t.transform.position, Vector3.up, Color.grey, 10);
		
		for(int i = 0; i < tiles.Count; i++){
			all.Remove(tiles[i]);
		}
			
		return all;
		
	}
	
	void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetAppendPosition(), 0.5f);
	}
	

    public List<Tile> GetRow(Tile t)
    {
       
        int rowNum = t.TilePos.z;
        List<Tile> tiles = new List<Tile>();

        if (Tiles == null) Tiles = FetchTiles();

        for (int i = 0; i < GridWidth; i++)
        {
            tiles.Add(Tiles[i, rowNum]);
        }

        return tiles    ;
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
    public List<Tile> GetTileList()
    {

        if (Tiles == null)
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
 }
