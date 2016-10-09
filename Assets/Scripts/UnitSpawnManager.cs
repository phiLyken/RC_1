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

    List<UnitSpawner> GetSpawnerForGroup(List<int> unique_ids, UnitSpawnGroupConfig group)
    {
        //if there are no distinct spawner ids left, stop
        if (unique_ids.Count == 0)
        {
            Debug.LogWarning("Not Enough SpawnGroups in Tileset for GroupCount");
            return null;
        }
        int id = group.SpawnerGroup == 0 ? MyMath.GetRandomObject(unique_ids) : group.SpawnerGroup;
        unique_ids.Remove(id);

       return Spawners.Where(sp => sp.SpawnerGroupID == id).ToList();
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

       // Debug.Log("spawning groups " + groups.Count);
        foreach (UnitSpawnGroupConfig group in groups) {


            globalGroupCounter++;

            List<UnitSpawner> spawnersForGroup = GetSpawnerForGroup(spawnerIDs, group);
            if(spawnersForGroup == null || spawnersForGroup.Count == 0)
            {
                Debug.Log("NO SPAWNERS FOR GROUP");
                return;
            }

            List<WeightedUnit> unitConfigs = RegionLoader.GetUnitsForGroupPower(group);

            if (unitConfigs.Count > spawnersForGroup.Count)
            {
                Debug.LogWarning("Not enough spawners for my group :(   spawners:" + spawnersForGroup.Count+"  units:"+ unitConfigs.Count);
              //  return;
            }

           // Debug.Log(spawnersForGroup.Count+ "   "+unitConfigs.Count);
            while(spawnersForGroup.Count > 0 && unitConfigs.Count > 0)
            {
               // Debug.Log("SPAWN");
                WeightedUnit unit = unitConfigs[Random.Range(0, unitConfigs.Count)];
                UnitSpawner spawner = spawnersForGroup[Random.Range(0, spawnersForGroup.Count)];

                unitConfigs.Remove(unit);
                spawnersForGroup.Remove(spawner);

                spawner.SpawnUnit( unit.UnitConfig,  unit.TurnTimeOnSpawn , globalGroupCounter);

            }
        }
    }
}
