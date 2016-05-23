﻿using UnityEngine;
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

    public void SpawnGroups(List<UnitSpawnGroupConfig> groups)
    {
        foreach(UnitSpawnGroupConfig group in groups) {
            List<WeightedUnit> unitconfigs = RegionLoader.GetUnitsForGroupPower(group);
            List<UnitSpawner> spawnersForId = Spawners.Where(sp => sp.SpawnerGroupID == group.SpawnerGroup).ToList();
            globalGroupCounter++;

            if(unitconfigs.Count > spawnersForId.Count)
            {
                Debug.LogWarning("Not enough spawners for my group :(   spawners:" + spawnersForId.Count+"  units:"+ unitconfigs.Count);
                return;
            }

            while(spawnersForId.Count > 0 && unitconfigs.Count > 0)
            {
                
                WeightedUnit unit = unitconfigs[Random.Range(0, unitconfigs.Count)];
                UnitSpawner spawner = spawnersForId[Random.Range(0, spawnersForId.Count)];

                unitconfigs.Remove(unit);
                spawnersForId.Remove(spawner);

                spawner.SpawnUnit( unit.UnitConfig, (int)unit.TurnTimeOnSpawn.Value(), globalGroupCounter);
            }
        }
    }
}
