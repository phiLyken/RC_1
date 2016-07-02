using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RegionConfig : ScriptableObject {

    public TileManager TileSet;
	public int RegionTotalEnemyPower;

    public List<UnitSpawnGroupConfig> Groups;
	
}

[System.Serializable]
public class UnitSpawnGroupConfig : IWeightable
{
    public int SpawnerGroup;
    public int GroupPower;
    public bool ForceGroup;

    public List<WeightedUnit> SpawnableUnits;
    public float _Weight;

    float IWeightable.Weight
    {
        get
        {
            return _Weight;
        }
        set
        {
            _Weight = value;
        }
    }
}

[System.Serializable]
public class WeightedUnit : IWeightable {

    public bool ForceUnit;
	public ScriptableUnitConfig UnitConfig;
	public float _Weight;
    public MyMath.R_Range TurnTimeOnSpawn;

    float IWeightable.Weight {
		get {
			return _Weight;
		}
		set {
			_Weight = value;
		}
	}

}

[System.Serializable]
public class WeightedRegion : IWeightable
{
    public RegionConfig Region;
    public float _Weight;
  
    public float Weight
    {
        get
        {
            return _Weight;
        }

        set
        {
           
        }
    }
}