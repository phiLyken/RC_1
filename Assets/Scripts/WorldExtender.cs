using UnityEngine;
using System.Collections;

public class WorldExtender : MonoBehaviour {
    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>

    public int MinTilesLastUnit;

    static  public int currentPhaseID;
    public int TilesUntilCamp;
    public int CampRangeIncreaseMin;
    public int CampRangeIncreaseMax;
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
        if (LastUnitCloseToEnd(TileManager.Instance.FirstUnitRow(0)))
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
            TilesUntilCamp += Random.Range(CampRangeIncreaseMin, CampRangeIncreaseMax);
        }
        else
        {
            region = RegionLoader.GetRegion();
        }
        currentPhaseID++;
        TileManager.Instance.AppendGrid(region);
    }

    bool LastUnitCloseToEnd(int last_unit_row)
    {
       //Debug.Log("last unit unit " + last_unit_row + "  height:" + TileManager.Instance.GridHeight);
        return (TileManager.Instance.GridHeight - last_unit_row) < MinTilesLastUnit;
    }
   
}
