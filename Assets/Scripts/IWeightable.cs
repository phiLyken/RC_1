using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IWeightable  {


	float Weight {
		get;
		set;
	}
	
}

[System.Serializable]
public class GenericWeightable<T> : IWeightable
{
    public float weight;
    public T Value;

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

    public GenericWeightable(float _weight, T obj)
    {
        weight = _weight;
        Value = obj;
    }

    public GenericWeightable()
    {

    }
   
}
