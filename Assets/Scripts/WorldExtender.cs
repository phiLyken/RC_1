﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldExtender : MonoBehaviour {
    public bool UseChoosenRegion;

    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>


     //keep track of spawned
    protected List<RegionConfig> spawned;

    public RegionConfigDataBase RegionBalance;

    public int MinTilesLastUnit;

    public int CurrentStageOverride;

    static public int CurrentStage;
    
    public int TilesUntilCamp;

    public static WorldExtender Instance;

    void Awake()
    {
        Instance = this;

    }
    void Start()
    {
       
        SetupGame();
    }

    protected virtual void SetupGame()
    {
        if (UseChoosenRegion)
            RegionBalance = GameManager.ChoosenRegion;

        CurrentStage = CurrentStageOverride;
        TurnSystem.Instance.OnGlobalTurn += OnGlobalTurn;
        spawned = new List<RegionConfig>();

        SpawnRegion(RegionLoader.GetStartRegion(RegionBalance), TileManager.Instance);
        SpawnNext();
    }

    void OnGlobalTurn(int crumble_row)
    {
        if (LastUnitCloseToEnd(TileManager.Instance.FirstUnitRow(0)))
        {
            SpawnNext();
        }
    }

    public static void SpawnRegion(RegionConfig region, TileManager target )
    {
        TileManager instance = Instantiate(region.TileSet).gameObject.GetComponent<TileManager>();
        target.AppendGrid(instance);
       
        //spawn the units and shit
        UnitSpawnManager spawner = instance.GetComponent<UnitSpawnManager>();

      
        List<UnitSpawnGroupConfig> groups = RegionLoader.GetGroupsForPower(region);

        if (region.SpawnSquad)
        {
            groups.Add(SquadManager.MakeSquadGroup());
        }

       // Debug.Log("Spawn Groups " + groups.Count);
        spawner.SpawnGroups(groups);
    }


    /// <summary>
    /// Decides what to spawn next (camp, or regular region) and starts the region spawning process
    /// </summary>
    public virtual void SpawnNext()
    {
        RegionConfig region = null;       

        if (TilesUntilCamp <= TileManager.Instance.GridHeight)
        {
            region = RegionLoader.GetCamp(RegionBalance, CurrentStage);
            CurrentStage++;
            TilesUntilCamp = GetNextCampSpawn();
        }
        else
        {
            region = RegionLoader.GetWeightedRegionForLevel(RegionBalance, CurrentStage, spawned);
            if (region == null)
                return;
            spawned.Add(region);
        }

      //  Debug.Log("spawning region " + region.name);
        SpawnRegion(region, TileManager.Instance);
    }

    bool LastUnitCloseToEnd(int last_unit_row)
    {
       //Debug.Log("last unit unit " + last_unit_row + "  height:" + TileManager.Instance.GridHeight);
        return (TileManager.Instance.GridHeight - last_unit_row) < MinTilesLastUnit;
    }

    int GetNextCampSpawn()
    {
        return TilesUntilCamp + 50+ Random.Range(0, 10); 
    }
   
}
