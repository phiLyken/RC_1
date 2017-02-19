using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SquadManager {

    public event Action<List<TieredUnit>> OnSelectedUpdate;
       
    public List<TieredUnit> selected_units;
    public List<ScriptableUnitConfig> evacuated;
    public List<ScriptableUnitConfig> killed;

    public bool DebugUseForceSelected;

    List<Unit> spawnedUnits;

    static SquadManager _instance;

    List<Unlockable<TieredUnit>> unitunlocks;
    List<Unlockable<SquadSizeConfig>> squadsizeunlocks;

    public static SquadManager Instance
    {
        get
        {
            return _instance == null ?
                 new SquadManager() : _instance;
        }
    }
    public List<Unlockable<TieredUnit>> GetSelectible()
    {
        return unitunlocks;
    }

    public SquadManager(List<TieredUnit> Tiers, List<SquadSizeConfig> SquadSizes, Levels levels, bool DEBUG_RANDOM_GROUP = false)
    {
        _instance = this;
        DebugUseForceSelected = DEBUG_RANDOM_GROUP;

        squadsizeunlocks = UnlockableFactory.MakeUnlockables<SquadSizeConfig>(SquadSizes, levels, SquadSizeConfig.GetRequirement, SquadSizeConfig.GetID);
        unitunlocks = UnlockableFactory.MakeUnlockables<TieredUnit>(Tiers, levels, TieredUnit.GetRequirement, TieredUnit.GetID);

        evacuated = new List<ScriptableUnitConfig>();
        killed = new List<ScriptableUnitConfig>();

        if (!DebugUseForceSelected)
        {
            ClearSelected();
        } else
        {            
            selected_units = new List<TieredUnit>( Tiers).GetRandomRemove(GetMaxSquadsize());
            OnSelectedUpdate.AttemptCall(selected_units);
        }

        Unit.OnEvacuated += Evacuated;
        Unit.OnUnitKilled += Killed;

 
    }

    public SquadManager() : this(PlayerLevel.Instance)
    { }
    public SquadManager(Levels levels) : this(
                        Resources.Load<ScriptableUnitTiers>("Units/unittiers_defaultbalancing").TieredUnitsSelectible,
                        Resources.Load<ScriptableUnitTiers>("Units/unittiers_defaultbalancing").SquadSizes,
                        levels, false
                            )
    {   
    }

    public void ClearSelected()
    {
        selected_units = new List<TieredUnit>();
    }
    void Killed(Unit u)
    {
        if(u.OwnerID == 0)
            killed.Add(u.Config);
    }

    void Evacuated(Unit u)
    {
        evacuated.Add(u.Config);
    }

    public static void RemoveFromSquad(TieredUnit conf)
    {
        Debug.Log("REMOVING " + conf.Tiers[0].Config.ID);
        Instance.selected_units.Remove(conf);
        Instance.OnSelectedUpdate.AttemptCall(Instance.selected_units);
    }

    public static bool AddToSquad(Unlockable<TieredUnit> conf)
    {
        if(conf.IsUnlocked() && !Instance.selected_units.Contains(conf.Item) && Instance.selected_units.Count < Instance.GetMaxSquadsize())
        {
            Debug.Log("ADDING " + conf.Item.Tiers[0].Config.ID);
            Instance.selected_units.Add(conf.Item);
            Instance.OnSelectedUpdate.AttemptCall(Instance.selected_units);
            return true;
        }
        ToastNotification.SetToastMessage2("Squad is Full");

        return false;
    }

    public List<TieredUnit> GetUnlockedTiers()
    {
        List<TieredUnit> tiers = unitunlocks.Where(unlock => unlock.IsUnlocked()).Select(unlock => unlock.Item).ToList();
        return tiers;
        
    }
    public int GetMaxSquadsize()
    {
        return squadsizeunlocks.GetHighestUnlocked<SquadSizeConfig>().Size;
    }

    public List<Unlockable<SquadSizeConfig>> GetSquadSizeUnlockes()
    {
        return squadsizeunlocks;
    }
    void StartGame(List<Unit> spawned)
    {
        spawnedUnits = spawned;
    }

    public static UnitSpawnGroupConfig MakeSquadGroup()
    {
        List<ScriptableUnitConfig> unit_configs = new List<ScriptableUnitConfig>();
        UnitSpawnGroupConfig conf = new UnitSpawnGroupConfig();
        conf.ForceGroup = true;
        conf.SpawnerGroup = 1;
        conf.SpawnableUnits = new List<WeightedUnit>();

        if (Instance.selected_units.IsNullOrEmpty())
        {
            Debug.LogWarning("NO UNITS SELECTED");

            Instance.unitunlocks.Select(unlock => unlock.Item).ToList().ForEach(unlock => unit_configs.Add(unlock.Tiers.GetRandom().Config));
        } else
        {
            foreach (var _selected in Instance.selected_units)
            {
                unit_configs.Add(TieredUnit.Unlocks(_selected.Tiers, PlayerLevel.Instance).GetHighestUnlocked().Config);
            }
        }
        

        List<ScriptableUnitConfig> selected = unit_configs.GetRandomRemove(Instance.GetMaxSquadsize());

        foreach (ScriptableUnitConfig unit_config in selected)
        {
            WeightedUnit unit = new WeightedUnit();
            unit.ForceUnit = true;
            unit.TurnTimeOnSpawn =  Constants.PLAYER_SQUAD_START_INITIATIVE;
            unit.UnitConfig = unit_config;
           
            conf.SpawnableUnits.Add(unit);
        }

        return conf;
    }

 

}
[System.Serializable]
public class TieredUnit
{
    public List<UnitTier> Tiers;
    
    public static List<Unlockable<UnitTier>> Unlocks(List<UnitTier> tiers, Levels levels)
    {
        List<Unlockable<UnitTier>> locked = new List<Unlockable<UnitTier>>();
        foreach(var tier in tiers)
        {
            locked.Add(new Unlockable<UnitTier>(tier, levels, tier.LevelRequirement, UnitTier.GetID));
        }
        return locked;
       
    }

    public static List<UnitTier> AllTiers(List<TieredUnit> TieredUnitsSelectible)
    {
        List<UnitTier> tiers = new List<UnitTier>();
        foreach (var v in TieredUnitsSelectible)
        {
            foreach (var t in v.Tiers)
            {
                tiers.Add(t);
            }
        }

        return tiers;
    }

    public static int GetRequirement(TieredUnit tier)
    {
        return tier.Tiers.Min(_tier => _tier.LevelRequirement);
    }

    public static string GetID(TieredUnit tier)
    {
        return tier.Tiers[0].Config.ID;
    }


}

[System.Serializable]
public class UnitTier
{   
 
    public ScriptableUnitConfig Config;
    public int LevelRequirement;
 
    public static int GetRequirement(UnitTier tier)
    {
        return tier.LevelRequirement;
    }

    public static string GetID(UnitTier tier)
    {
        return tier.Config.name;
    }

   
 
}

[System.Serializable]
public class SquadSizeConfig
{
    public int Size;
    public int Level;

    public static int GetRequirement(SquadSizeConfig config)
    {
        return config.Level;
    }

    public static string GetID(SquadSizeConfig config)
    {
        return "Squad Size " + config.Size;
    }
}
