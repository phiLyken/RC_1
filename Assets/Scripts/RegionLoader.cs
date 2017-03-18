using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class RegionLoader  {
    


    public static List<RegionConfig> RegionsToSpawn(RegionConfigDataBase config)
    {
        List<RegionConfig> regions_to_spawn = new List<RegionConfig>();
        regions_to_spawn.Add(config.StartRegion);

        foreach (var pool in config.AllPools)
        {
            regions_to_spawn.Add(M_Weightable.GetWeighted(pool.Regions).Region);
        }

        regions_to_spawn.Add(M_Weightable.GetWeighted(config.Camps).Region);

        return regions_to_spawn;

    }

 

    public static List<WeightedUnit> GetUnitsForGroupPower(UnitSpawnGroupConfig group) { 

        int powerLeft = group.GroupPower;

        List<WeightedUnit> possibleUnits = group.SpawnableUnits.Where(u => !u.ForceUnit).ToList();
        List<WeightedUnit> choosenUnits = group.SpawnableUnits.Where(u => u.ForceUnit).ToList();

        List<WeightedUnit> validUnits = GetUnitsInPowerRange(possibleUnits, powerLeft);

        while(validUnits.Count > 0)
        {
            WeightedUnit choosen =  M_Weightable.GetWeighted(validUnits) ;        
            choosenUnits.Add(choosen);
            int unitPower = choosen.UnitConfig.UnitPower;
            if(unitPower <= 0)
            {
                Debug.LogWarning("UNIT POWER 0, aborting to avoid infinite loop");
                return null;
            }

            powerLeft -= unitPower;
            
            validUnits = GetUnitsInPowerRange(possibleUnits, powerLeft);
        }
     
        return choosenUnits;
    }

    public static List<UnitSpawnGroupConfig> GetGroupsForPower(RegionConfig region)
    {
       // MDebug.Log("Get groups for " + region);
        //Add all forced groups to choosen ones
        List<UnitSpawnGroupConfig> choosenGroups = region.Groups.Where(gr => gr.ForceGroup).ToList() ;

        //make a copy of groups but exclude the ones that we already have
        List<UnitSpawnGroupConfig> groupsCopy = new List<UnitSpawnGroupConfig>(region.Groups).Where(gr => !choosenGroups.Contains(gr)).ToList();
        
        int powerLeft = region.RegionTotalEnemyPower;
        //list of groupls that are valid for the powerlevel left
        List<UnitSpawnGroupConfig> validgroups = GetGroupsInPowerLevel(groupsCopy, powerLeft);       

        while (validgroups.Count > 0)
        {
            UnitSpawnGroupConfig choosen = M_Weightable.GetWeighted(validgroups);
            choosenGroups.Add(choosen);
            groupsCopy.Remove(choosen);
            int groupPower = choosen.GroupPower;

            if (groupPower <= 0)
            {
                Debug.LogWarning("UNIT POWER 0, aborting to avoid infinite loop");
                return choosenGroups;
            }

            powerLeft -= groupPower;

            if(groupsCopy.Count == 0)
            {
                Debug.LogWarning("No groups to spawn left");
                return choosenGroups;
            }
            validgroups = GetGroupsInPowerLevel(groupsCopy, powerLeft);
        }

        return choosenGroups;
    }
    static List<WeightedUnit> GetUnitsInPowerRange( List<WeightedUnit> units, int power_threshold)
    {
        return units.Where(wr => wr.UnitConfig.UnitPower <= power_threshold).ToList();
    }

    static List<UnitSpawnGroupConfig> GetGroupsInPowerLevel(List<UnitSpawnGroupConfig> groups, int power_threshold)
    {
        return groups.Where(gr => gr.GroupPower <= power_threshold).ToList();
    }


 
}
