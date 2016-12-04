using UnityEngine;
using System.Collections;
using System;

public abstract class CameraAction : MonoBehaviour
{
   
    public bool IsBlocking;
    public bool ResetOthers;

    public enum CameraActionType
    {
        move, rotate, zoom
    }

    public bool Active;
  
    public CameraActionType Type;

    protected Action<CameraAction> ResetCallback;
    protected Func<CameraAction, bool> canstartInput;

    public virtual void Init(Func<CameraAction, bool> _inputEnabled, Action<CameraAction> reset_callback)
    {
        canstartInput = _inputEnabled;
        ResetCallback = reset_callback;

    }

    protected virtual bool CanStartInput()
    {
        return canstartInput(this);
    }



    public abstract void Stop();
    
}