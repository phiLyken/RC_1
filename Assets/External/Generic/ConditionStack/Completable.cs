using UnityEngine;
using System.Collections;
using System;

public class Completable : MonoBehaviour, ICompletable
{
    public KeyCode press_to_complete;
    bool completed = false;

    public event System.Action OnCancel;
    public event System.Action OnComplete;

    public bool GetComplete()
    {
        return completed;
    }

    public void Reset()
    {
        completed = false;
    }

    void Update()
    {
        if (GetComplete())
        {
            completed = true;
            OnComplete.AttemptCall();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            OnCancel.AttemptCall();
        }
    }
}