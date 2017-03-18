using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileWeighted : IWeightable {

	public float weight;
	public TilePos tilePos;

    public static List<Tile> GetCrumbleTiles(int count, TileManager region)
    {
        return (from wt 
                in M_Weightable.GetWeighted( GetWeightedTiles(region).Cast<IWeightable>().ToList() , count)
                select region.Tiles[ (wt as TileWeighted).tilePos.x, (wt as TileWeighted).tilePos.z]).ToList();

    }

    public static List<TileWeighted> GetWeightedTiles(TileManager region)
    {
        List<TileWeighted> weightedTiles = new List<TileWeighted>();
        foreach (Tile t in region.GetTileList())
        {
            weightedTiles.Add(new TileWeighted(t, region));
        }

        weightedTiles.Shuffle();
        return weightedTiles;
    }

	public TileWeighted(Tile tile, TileManager region){
        tilePos = tile.TilePos;


        weight = 0;

        if (!tile.isEnabled || tile.isCrumbling ) return;

        //Apply weight based on distance to last row (the closer the more weight)
        int lastRow = region.GetLastActiveRow();

		int max_rows = Constants.CrumbleRange;

        int distance = max_rows - Mathf.Min(tile.TilePos.z - lastRow, max_rows);

        float prev_weight = 0;
        float neighbours_weight = 0;

		//apply weight if exposed to previous row
		if(tile.TilePos.z > 0){
            prev_weight = GetNeighbourWeight(region.Tiles[ tile.TilePos.x, tile.TilePos.z-1], Constants.CRUMBLE_WEIGHT_DEPTH_MULTIPLER);
		} else
        {
            prev_weight = Constants.CRUMBLE_WEIGHT_INACESSIBLE_WEIGHT;     
       
        }

		//apply weight for left/right neighbours
		if(tile.TilePos.x > 0){
            neighbours_weight += GetNeighbourWeight(region.Tiles[tile.TilePos.x -1, tile.TilePos.z], Constants.CRUMBLE_WEIGHT_WIDTH_MODIFIER);
		}
		if(tile.TilePos.x < region.Tiles.GetLength(0) -1){
            neighbours_weight += GetNeighbourWeight(region.Tiles[tile.TilePos.x +1, tile.TilePos.z], Constants.CRUMBLE_WEIGHT_WIDTH_MODIFIER);
		}

        weight = distance * Constants.CRUMBLE_WEIGHT_DISTANCE + distance * (neighbours_weight + prev_weight);
       // weight = distance;
	}

    public float Weight
    {
        get   {  return weight; }

        set  {
        }
    }

    public string WeightableID
    {
        get
        { return tilePos.x+" "+tilePos.z; }
    }

    float GetNeighbourWeight(Tile t, float multiplier){


		return (!t.isAccessible ?
            Constants.CRUMBLE_WEIGHT_INACESSIBLE_WEIGHT :
            ( Mathf.Abs(t.CrumbleStage) * Constants.CRUMBLE_WEIGHT_STAGE_WEIGHT)) * multiplier;
	}

    

}
