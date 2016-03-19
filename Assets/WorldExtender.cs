using UnityEngine;
using System.Collections;

public class WorldExtender : MonoBehaviour {
    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>
    public int MinTiles;

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
            TileManager.Instance.AppendGrid(RegionLoader.GetRegion());
        }
    }

    bool ShouldSpawnRegion(int crumble_row)
    {
        return (TileManager.Instance.GridHeight - crumble_row) < MinTiles;
    }
}
