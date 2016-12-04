using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class StrategyCamera : MonoBehaviour
{

    bool inputEnabled;

    public static StrategyCamera Instance;
    public List<CameraAction> Actions;

    public CameraAction_DirectionPan ActionPanDirection;
    public CameraAction_Rotate ActionRotate;
    public CameraAction_Pan ActionPan;
    public CameraAction_PanToPosition ActionPanToPos;
    public CameraAction_Zoom ActionZoom;
    

    public void DisableInput()
    {
        inputEnabled = false;
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }
    void Awake()
    {
        Instance = this;
        Actions.Add(ActionRotate);
        Actions.Add(ActionPan);
        Actions.Add(ActionPanToPos);
        Actions.Add(ActionZoom);
        Actions.Add(ActionPanDirection);

        Actions.ForEach(action => action.Init(CanStartInput, ResetOthers));
        inputEnabled = true;
    }

    public void ResetOthers(CameraAction requester)
    {
        Actions.Where(action => action != requester).ToList().ForEach(action => action.Stop());
    }

    public bool CanStartInput(CameraAction action)
    {
       return inputEnabled && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !HasBlockingAction();
    }

    public static bool HasBlockingAction()
    {
        return Instance != null && Instance.Actions.Count(action => action.IsBlocking && action.Active) > 0;
    }    
    
    public static bool CameraActionInProgress()
    {
        return Instance != null && Instance.Actions.Count(action => action.Active) > 0;
    }
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, MyMath.GetCameraCenter());
    }
}
