using UnityEngine;
using System.Collections;

public interface IWeightable  {

	string WeightableID {
		get;

	}

	float Weight {
		get;
		set;
	}
	
}
