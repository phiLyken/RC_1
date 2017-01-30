using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour {

    public M_Math.R_Range StartTurnTime;
    public int SquadSize;
    public List<ScriptableUnitConfig> squad_units;

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
       // squad_units = new List<ScriptableUnitConfig>();
        instance = this;
    }

    void Awake()
    {
        if(instance == null)
        {
            init();
        }
    }

    public static void RemoveFromSquad(ScriptableUnitConfig conf)
    {
        Instance.squad_units.Remove(conf);
    }

    public static void AddToSquad(ScriptableUnitConfig conf)
    {
        if(!Instance.squad_units.Contains(conf) && Instance.squad_units.Count <= GetMaxSquadsize() )
             Instance.squad_units.Add(conf);
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

        List<ScriptableUnitConfig> unit_configs = new List<ScriptableUnitConfig>(Instance.squad_units);
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
