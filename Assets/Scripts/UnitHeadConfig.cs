using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UnitHeadConfig  {

    public List<HeadData> Heads;

    public HeadData GetHead()
    {
        return WeightableFactory.GetWeighted(Heads);
    }   
}
[System.Serializable]
public class HeadData : IWeightable
{
    public Sprite UI_Texture;
    public GameObject Mesh;
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