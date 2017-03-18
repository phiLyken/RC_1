using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class UnitSpawnManager : MonoBehaviour {
    
    List<UnitSpawner> Spawners;
    static int globalGroupCounter;

    void Awake()
    {        
        Spawners = ( GetComponentsInChildren<UnitSpawner>()).ToList();        
    }
    public List<int> GetSpawnersIDs(List<UnitSpawner> Spawners)
    {
        List<int> ids = new List<int>();

        //Add distinct id's
       foreach(UnitSpawner sp in Spawners)
        {
            if (!ids.Contains(sp.SpawnerGroupID))
            {
                ids.Add(sp.SpawnerGroupID);
            }
        }

        return ids;

    }
    public List<UnitSpawner> GetSpawners(List<int> unique_ids, int for_id)
    {
        int id = for_id == 0 ? M_Math.GetRandomObject(unique_ids) : for_id;
        unique_ids.Remove(id);
        return GetSpawners(id);
       
    }

    public List<UnitSpawner> GetSpawners(int for_id)
    {
        return Spawners.Where(sp => sp.SpawnerGroupID == for_id).ToList();
    }

    public List<UnitSpawner> GetSpawnerForGroup(List<int> unique_ids, UnitSpawnGroupConfig group)
    {
        //if there are no distinct spawner ids left, stop
        if (unique_ids.Count == 0)
        {
            Debug.LogWarning("Not Enough SpawnGroups in Tileset for GroupCount");
            return null;
        }

        if(group == null)
        {
            Debug.LogWarning("No Group");
            return null;
        }

        return GetSpawners(unique_ids, group.SpawnerGroup);
    }
   
    public void SpawnGroups(List<UnitSpawnGroupConfig> groups)
    {
        //first spawn groups with fixed spawner ids
        // -> sort descending by spawner ID, hence "0" groups will be last
        // get a list of all spawnergroups for an id
        // remove a spawner when it has been chosen
        // then spawn the groups without spawner and choose randomly from the remaining spawners until no more spawners
        // or no more groups are left
      
        groups = groups.OrderByDescending(gr => gr.SpawnerGroup).ToList();

        List<int> spawnerIDs = GetSpawnersIDs(Spawners);

       // MDebug.Log("spawning groups " + groups.Count);
        foreach (UnitSpawnGroupConfig group in groups) {
            SpawnGroup(group, spawnerIDs);
        }
    }

    void SpawnGroup(UnitSpawnGroupConfig group, List<int> spawnerIDs)
    {
        if(group == null)
        {
            Debug.LogWarning("No Group");
            return;
        }

        globalGroupCounter++;

        List<UnitSpawner> spawnersForGroup = GetSpawnerForGroup(spawnerIDs, group);

        if (spawnersForGroup == null || spawnersForGroup.Count == 0)
        {
            Debug.LogWarning("NO SPAWNERS FOR GROUP");
            return;
        }

        List<WeightedUnit> unitConfigs = RegionLoader.GetUnitsForGroupPower(group);

        if (unitConfigs.Count > spawnersForGroup.Count)
        {
            Debug.LogWarning("Not enough spawners for my group :(   spawners:" + spawnersForGroup.Count + "  units:" + unitConfigs.Count);
            //  return;
        }

        // MDebug.Log(spawnersForGroup.Count+ "   "+unitConfigs.Count);
        while (spawnersForGroup.Count > 0 && unitConfigs.Count > 0)
        {
            // MDebug.Log("SPAWN");
            WeightedUnit unit = unitConfigs.GetRandom();
            UnitSpawner spawner = spawnersForGroup.GetRandom();

            unitConfigs.Remove(unit);
            spawnersForGroup.Remove(spawner);

            spawner.SpawnUnit(unit.UnitConfig, unit.TurnTimeOnSpawn, globalGroupCounter, unit.HidePlayerUnit);

        }
    }

 
}
