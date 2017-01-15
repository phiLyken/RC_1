using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Randomizer : MonoBehaviour
{

    public float IntervalMin;
    public float IntervalMax;

    public Action<float> SetValue;


    public void Init(Action<float> _setValue)
    {

    }

    void Update()
    {

    }
}

public class RandomColor : RandomizerValues<Color>
{



}




public class RandomizerValues<T> : Randomizer
{

    public List<T> Values;

    public void Init(Action<T> _setValue, Func<T, float> _getValue)
    {

    }

}