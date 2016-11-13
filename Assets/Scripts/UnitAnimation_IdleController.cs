using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

 
public class UnitAnimation_IdleController : MonoBehaviour {

    public List<IdleConfig> IdleRegular;
    public List<IdleConfig> IdleRush;
	// Use this for initialization

    public int GetId(bool raged)
    {
        List<IdleConfig> list = raged ? IdleRush : IdleRegular;
 
        return WeightableFactory.GetWeighted(list).Index;
    }
}

[System.Serializable]
public class IdleConfig : IWeightable
{
    public int Index;
    public float weight;

    float IWeightable.Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }
}