using UnityEngine;
using System.Collections;

public class WorldExtender : MonoBehaviour {
    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>
    public int MintTilesCrumble;
    public int MinTilesLastUnit;

    static  public int currentPhaseID;
    public int TilesUntilCamp;
    
    void Start()
    {
        TurnSystem.Instance.OnGlobalTurn += OnGlobalTurn;
        SetupGame();
    }

    void SetupGame()
    {
        TileManager.Instance.AppendGrid(RegionLoader.GetRegion());
    }

    void OnGlobalTurn(int crumble_row)
    {
        if (LastUnitCloseToEnd(TileManager.Instance.FirstUnitRow()))
        {
            SpawnNext();
        }
    }
    void SpawnNext()
    {
        TileManager region = null;
        if (TilesUntilCamp <= TileManager.Instance.GridHeight)
        {
            region = RegionLoader.GetCamp();
            TilesUntilCamp += Random.Range(40, 80);
        }
        else
        {
            region = RegionLoader.GetRegion();
        }
        currentPhaseID++;
        TileManager.Instance.AppendGrid(region);
    }

    bool HasEnoughSpace(int crumble_row)
    {
        return (TileManager.Instance.GridHeight - crumble_row) < MintTilesCrumble;
    }

    bool LastUnitCloseToEnd(int last_unit_row)
    {
       //Debug.Log("last unit unit " + last_unit_row + "  height:" + TileManager.Instance.GridHeight);
        return (TileManager.Instance.GridHeight - last_unit_row) < MinTilesLastUnit;
    }
   
}
