using UnityEngine;
using System.Collections;

public class WorldExtender : MonoBehaviour {
    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>
    public int MinTiles;
    static  public int currentPhaseID;
    public int TilesUntilCamp;
    
    void Start()
    {
        if( WorldCrumbler.Instance != null)
        {
            WorldCrumbler.Instance.OnCrumble += OnCrumble;
        }

        SetupGame();
    }

    void SetupGame()
    {
        TileManager.Instance.AppendGrid(RegionLoader.GetRegion());
    }
    void OnCrumble(int crumble_row)
    {
        if(ShouldSpawnRegion(crumble_row))
        {
            TileManager region = null;
            if(TilesUntilCamp <= TileManager.Instance.GridHeight)
            {
                region = RegionLoader.GetCamp();
                TilesUntilCamp += Random.Range(40, 80);
            } else
            {
                region = RegionLoader.GetRegion();
            }
            currentPhaseID++;
            TileManager.Instance.AppendGrid(region);
        }
    }

    bool ShouldSpawnRegion(int crumble_row)
    {
        return (TileManager.Instance.GridHeight - crumble_row) < MinTiles;
    }

   
}
