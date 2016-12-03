using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class RegionLoader  {

    public static RegionConfig GetStartRegion(RegionConfigDataBase regions)
    {
        return regions.StartRegion;
    }

    public static RegionConfig GetWeightedRegionForLevel(RegionConfigDataBase regions, int level, List<RegionConfig> exclude)
    {

        RegionPool pool_level =  regions.GetPool(level);
        if(pool_level == null)
        {
            Debug.LogWarning(" NO REGIONS LEFT for LEVEL " + level);
            return null;
        }
        // get regions for level
        List<WeightedRegion> configs = pool_level.Regions.Where( r =>  !exclude.Contains(r.Region ) ).ToList();
        
        if(configs.Count == 0)
        {
            Debug.LogWarning(" NO REGIONS LEFT for LEVEL " + level);
            return null;
        }

       //  Debug.Log(configs.Count);
        // choose random region by weight
        WeightedRegion wr = WeightableFactory.GetWeighted(configs);

        if (wr == null)
            return null;
        
        return wr.Region;
            
    }


    public static RegionPool GetPoolConfi(RegionConfigDataBase regions, int index)
    {
        RegionConfigDataBase db = regions;

        if (index >= db.AllPools.Count)
            Debug.LogWarning("Not sufficient index for Region Pools " + index + " items in pool" + db.AllPools.Count);

        return db.AllPools[Mathf.Min(index, db.AllPools.Count - 1)];

    }

    public static List<WeightedUnit> GetUnitsForGroupPower(UnitSpawnGroupConfig group) { 

        int powerLeft = group.GroupPower;

        List<WeightedUnit> possibleUnits = group.SpawnableUnits.Where(u => !u.ForceUnit).ToList();
        List<WeightedUnit> choosenUnits = group.SpawnableUnits.Where(u => u.ForceUnit).ToList();

        List<WeightedUnit> validUnits = GetUnitsInPowerRange(possibleUnits, powerLeft);

        while(validUnits.Count > 0)
        {
            WeightedUnit choosen =  WeightableFactory.GetWeighted(validUnits) ;        
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
       // Debug.Log("Get groups for " + region);
        //Add all forced groups to choosen ones
        List<UnitSpawnGroupConfig> choosenGroups = region.Groups.Where(gr => gr.ForceGroup).ToList() ;

        //make a copy of groups but exclude the ones that we already have
        List<UnitSpawnGroupConfig> groupsCopy = new List<UnitSpawnGroupConfig>(region.Groups).Where(gr => !choosenGroups.Contains(gr)).ToList();
        
        int powerLeft = region.RegionTotalEnemyPower;
        //list of groupls that are valid for the powerlevel left
        List<UnitSpawnGroupConfig> validgroups = GetGroupsInPowerLevel(groupsCopy, powerLeft);       

        while (validgroups.Count > 0)
        {
            UnitSpawnGroupConfig choosen = WeightableFactory.GetWeighted(validgroups);
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


    public static RegionConfig GetCamp(RegionConfigDataBase region_balance, int level)
    {
        return WeightableFactory.GetWeighted(region_balance.GetPool(level).Camps).Region;
       
    }
}
