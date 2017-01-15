using UnityEngine;
using System.Collections;
using System;

public abstract class CameraAction : MonoBehaviour
{
    protected Transform Bounds;
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

    public virtual void Init(Func<CameraAction, bool> _inputEnabled, Action<CameraAction> reset_callback, Transform Bounds)
    {
        canstartInput = _inputEnabled;
        ResetCallback = reset_callback;

    }

   
    public void SetBounds(Transform tr)
    {
        Bounds = tr;
    }

    protected virtual bool CanStartInput()
    {
        return canstartInput(this);
    }

    protected  void AttemptMove(Vector3 move)
    {
        if (Bounds != null)
        {
            transform.AttemptMoveInBounds(Bounds.Bounds(), move, M_Math.GetCameraCenter());
        }
        else
        {
            transform.Translate(move, Space.World);
        }
    }

    public abstract void Stop();
    
}