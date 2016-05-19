using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionConfig : IWeightable {

	public GameObject TileSetObject;
	public int RegionTotalEnemyPower;

	public float _Weight;
	public List<WeightedUnit> SpawnAbleUnits;
	#region IWeightable implementation

	float IWeightable.Weight {
		get {
			return _Weight;
		}
		set {
			_Weight = value;
		}
	}
	#endregion
}


[System.Serializable]
public class WeightedUnit : IWeightable {
	
	public string UnitID;
	public float _Weight;

	float IWeightable.Weight {
		get {
			return _Weight;
		}
		set {
			_Weight = value;
		}
	}


}
