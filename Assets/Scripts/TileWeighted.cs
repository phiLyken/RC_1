using UnityEngine;
using System.Collections;

public class TileWeighted  {

	public float weight;
	TilePos tile;

	public TileWeighted(Tile tile, TileManager region){

		//If the tile is already crumbling it should be considered
		if(tile.CrumbleStage > 0 || !tile.isAccessible) weight = 0;


		weight = 0;

		//Apply weight based on distance to last row (the closer the more weight)
		int lastRow = region.GetLastActiveRow();
		int max_rows = 5;
		int distance = Mathf.Max(0, max_rows - ( tile.TilePos.z - lastRow)) ;
		weight = distance * 1;

		//apply weight if exposed to previous row
		if(tile.TilePos.z > 0){	
			weight += GetNeighbourWeight(region.Tiles[ tile.TilePos.z-1, tile.TilePos.x], 5);
		}

		//apply weight for left/right neighbours
		if(tile.TilePos.x > 0){
			weight += GetNeighbourWeight(region.Tiles[tile.TilePos.x -1, tile.TilePos.z], 1);
		}
		if(tile.TilePos.x < region.Tiles.GetLength(1) -1){
			weight += GetNeighbourWeight(region.Tiles[tile.TilePos.x +1, tile.TilePos.z], 1);
		}
	
	}

	float GetNeighbourWeight(Tile t, float multiplier){
		return (!t.isAccessible ? 15 : (t.CrumbleStage * 5)) * multiplier;
	}


}
