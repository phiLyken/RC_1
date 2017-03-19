using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldExtender : MonoBehaviour {
    

    /// <summary>
    /// Tiles left in the grid under consideration of the crumble-row when a new region is spawned
    /// </summary>


     //keep track of spawned
    protected List<RegionConfig> spawned;

    protected RegionConfigDataBase RegionBalance;

    public int MinTilesLastUnit;
    
    public int TilesUntilCamp;

    public static WorldExtender Instance;

    List<RegionConfig> configsToSpawn;

    public int GetGetDifficulty()
    {
        return RegionBalance.Difficulty;
    }
    public virtual void SetupGame(RegionConfigDataBase Region)
    {

        RegionBalance = Region;
        Instance = this;

        if(TurnSystem.Instance != null)
            TurnSystem.Instance.OnGlobalTurn += OnGlobalTurn;

        spawned = new List<RegionConfig>();

        SetSpawnConfigs(Region);
        SpawnNext();
        SpawnNext();
        PlayAmbient();

    }
    protected void PlayAmbient()
    {
        if (RegionBalance.AmbientSound != null)
        {
            Sound_Play ambient = Instantiate(Resources.Load("Sounds/sound_ambient") as GameObject).GetComponent<Sound_Play>();
            ambient.Play(RegionBalance.AmbientSound);
        }
    }
    protected void SetSpawnConfigs(RegionConfigDataBase Region)
    {
        configsToSpawn = RegionLoader.RegionsToSpawn(Region);
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
        
       // MDebug.Log("Spawn Groups " + groups.Count);
        spawner.SpawnGroups(groups);
    }


    /// <summary>
    /// Decides what to spawn next (camp, or regular region) and starts the region spawning process
    /// </summary>
    public virtual void SpawnNext()
    {
        if (configsToSpawn.IsNullOrEmpty()) {
            return;
        }
        
        RegionConfig region = null;

        region = configsToSpawn[0];
        configsToSpawn.RemoveAt(0);

        if (region == null)
            return;
        spawned.Add(region);
        

      //  MDebug.Log("spawning region " + region.name);
        SpawnRegion(region, TileManager.Instance);
    }

    bool LastUnitCloseToEnd(int last_unit_row)
    {
       //MDebug.Log("last unit unit " + last_unit_row + "  height:" + TileManager.Instance.GridHeight);
        return (TileManager.Instance.GridHeight - last_unit_row) < MinTilesLastUnit;
    }

    int GetNextCampSpawn()
    {
        return TilesUntilCamp + 50+ Random.Range(0, 10); 
    }
   
}
