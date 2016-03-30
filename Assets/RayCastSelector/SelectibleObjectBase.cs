using UnityEngine;
using System.Collections;


public delegate void EventHandler();
public class SelectibleObjectBase : MonoBehaviour {

    
	protected bool bTouchHold;
	protected float TouchStartTime;
    public EventHandler OnSelect;
    public EventHandler OnHover;
    public EventHandler OnHoverEnd;

    bool inFocus;

    void Update()
    {
        if (TurnSystem.Instance.Current.GetTurnControllerID() != 0) return;
        if(inFocus && !PanCamera.CameraAction && Input.GetButtonUp("Fire1"))
        {
            Select();
        }
    }
    void OnMouseOver()
    {

        if (TurnSystem.Instance.Current.GetTurnControllerID() != 0) return;
        if (!inFocus)
        {
            if (OnHover != null) OnHover();
        }
        inFocus = true;
       
      
    }
    void Select()
    {
        if (TurnSystem.Instance.Current.GetTurnControllerID() != 0) return;
        if (OnSelect != null) OnSelect(); 
    }


    void OnMouseExit()
    {
        if (TurnSystem.Instance.Current.GetTurnControllerID() != 0) return;
        inFocus = false;
        if (OnHoverEnd != null) OnHoverEnd();
       
    }

}
