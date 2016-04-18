using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public delegate void EventHandler();
public class SelectibleObjectBase : MonoBehaviour, IPointerClickHandler {

    
	protected bool bTouchHold;
	protected float TouchStartTime;
    public EventHandler OnSelect;
    public EventHandler OnHover;
    public EventHandler OnHoverEnd;

    bool inFocus;

    bool Cheat;


    void OnMouseOver()
    {

        if (!HumanInput()) return;
        if (!inFocus)
        {
            if (OnHover != null) OnHover();
        }
        inFocus = true;
       
      
    }
    void Select()
    {
        if (!HumanInput()) return;
        if (OnSelect != null) OnSelect(); 
    }


    void OnMouseExit()
    {
        if (!HumanInput()) return;
        inFocus = false;
        if (OnHoverEnd != null) OnHoverEnd();
       
    }

    bool HumanInput()
    {
        return TurnSystem.Instance == null || TurnSystem.Instance.Current == null || TurnSystem.Instance.Current.GetTurnControllerID() == 0 || Cheat;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            Cheat = !Cheat;
        }
        if (!HumanInput()) return;

        if (inFocus && !PanCamera.CameraAction && Input.GetMouseButtonUp(0))
        {

            Select();
        }
    }
}
