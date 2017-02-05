using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour {

    public List<TieredUnit> TieredUnitsSelectible;

    public M_Math.R_Range StartTurnTime;
    public int SquadSize;

    public List<ScriptableUnitConfig> selected_units;
    public List<ScriptableUnitConfig> evacuated;
    public List<ScriptableUnitConfig> killed;

    public bool DebugUseForceSelected;

    List<Unit> spawnedUnits;

    static SquadManager instance;

    public static List<ScriptableUnitConfig> GetPlayerSquadToSpawn;

    public static SquadManager Instance
    {
        get
        {
            if (instance == null)
            {
                FindObjectOfType<SquadManager>().init();

            }
            return instance;
        }
    }
    
    void init()
    {
        if(instance != null)
        {
            return;
        }

        evacuated = new List<ScriptableUnitConfig>();
        killed = new List<ScriptableUnitConfig>();

        if(!DebugUseForceSelected)
            selected_units = new List<ScriptableUnitConfig>();

        Unit.OnEvacuated += Evacuated;
        Unit.OnUnitKilled += Killed;

        instance = this;
    }

    void Awake()
    {
        if(instance == null)
        {
            init();
        } else
        {
            killed = new List<ScriptableUnitConfig>();
            evacuated = new List<ScriptableUnitConfig>();
        }
    }
    void Killed(Unit u)
    {
        killed.Add(u.Config);
    }

    void Evacuated(Unit u)
    {
        evacuated.Add(u.Config);
    }

    public static void RemoveFromSquad(ScriptableUnitConfig conf)
    {
        Instance.selected_units.Remove(conf);
    }

    public static void AddToSquad(ScriptableUnitConfig conf)
    {
        if(!Instance.selected_units.Contains(conf) && Instance.selected_units.Count <= GetMaxSquadsize() )
             Instance.selected_units.Add(conf);
    }

    public static int GetMaxSquadsize()
    {
        return instance.SquadSize;
    }

    void StartGame(List<Unit> spawned)
    {
        spawnedUnits = spawned;
    }

    public static UnitSpawnGroupConfig MakeSquadGroup()
    {
        UnitSpawnGroupConfig conf = new UnitSpawnGroupConfig();

        conf.ForceGroup = true;
        conf.SpawnerGroup = 1;
        conf.SpawnableUnits = new List<WeightedUnit>();

        List<ScriptableUnitConfig> unit_configs = new List<ScriptableUnitConfig>(Instance.selected_units);
        List<ScriptableUnitConfig> selected = unit_configs.GetRandomRemove(GetMaxSquadsize());

        foreach (ScriptableUnitConfig unit_config in selected)
        {
            WeightedUnit unit = new WeightedUnit();
            unit.ForceUnit = true;
            unit.TurnTimeOnSpawn =  instance.StartTurnTime;
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
}

[System.Serializable]
public class UnitTier
{
    public ScriptableUnitConfig Config;
    public int LevelRequirement;
}

