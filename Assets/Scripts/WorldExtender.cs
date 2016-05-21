using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldExtender : MonoBehaviour {
    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>
    public int MinTilesLastUnit;

    static public int CurrentStage;
    public int TilesUntilCamp;

    void Start()
    {
      //  TurnSystem.Instance.OnGlobalTurn += OnGlobalTurn;
        SetupGame();
    }

    void SetupGame()
    {

        SpawnRegion(RegionLoader.GetStartRegion(), TileManager.Instance);
    }

    void OnGlobalTurn(int crumble_row)
    {
        if (LastUnitCloseToEnd(TileManager.Instance.FirstUnitRow(0)))
        {
            SpawnNext();
        }
    }

    public static void SpawnRegion(RegionConfig region, TileManager target)
    {
        TileManager instance = Instantiate(region.TileSet).gameObject.GetComponent<TileManager>();

        target.AppendGrid(instance);

        //spawn the units and shit
        UnitSpawnManager spawner = instance.GetComponent<UnitSpawnManager>();
        List<UnitSpawnGroupConfig> groups = RegionLoader.GetGroupsForPower(region);

        spawner.SpawnGroups(groups);
     
    }


    /// <summary>
    /// Decides what to spawn next (camp, or regular region) and starts the region spawning process
    /// </summary>
    void SpawnNext()
    {
        RegionConfig region = null;       

        if (TilesUntilCamp <= TileManager.Instance.GridHeight)
        {
            region = RegionLoader.GetCamp(CurrentStage);
            CurrentStage++;
            TilesUntilCamp = GetNextCampSpawn();
        }
        else
        {
            region = RegionLoader.GetWeightedRegionForLevel(CurrentStage);
        }

        SpawnRegion(region, TileManager.Instance);
    }

    bool LastUnitCloseToEnd(int last_unit_row)
    {
       //Debug.Log("last unit unit " + last_unit_row + "  height:" + TileManager.Instance.GridHeight);
        return (TileManager.Instance.GridHeight - last_unit_row) < MinTilesLastUnit;
    }

    int GetNextCampSpawn()
    {
        return 200;
    }
   
}
