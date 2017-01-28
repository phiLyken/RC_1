using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectOnScreenEvents : MonoBehaviour {

    public Bounds Frame;

    public event Action OnAppear;
    public event Action OnDisappeared;


    void Update()
    {

        if (Frame.Contains(transform.GetRelativeScreenPos(Camera.main))){
            OnAppear.AttemptCall();
        } else
        {
            OnDisappeared.AttemptCall();
        }
    }

    void OnDestroy()
    {
        OnAppear = null;
        OnDisappeared = null;
    }
        
}
