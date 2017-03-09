using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public delegate void EventHandler();
public class SelectibleObjectBase : MonoBehaviour {

    
	protected bool bTouchHold;
	protected float TouchStartTime;
    public EventHandler OnSelect;
    public EventHandler OnHover;
    public EventHandler OnHoverEnd;

    bool inFocus;

    bool Cheat;

    void OnMouseOver()
    {
        if ( EventSystem.current.IsPointerOverGameObject())
            return;

        
      
        if (HumanInput() && !inFocus)
        {
          //  Debug.Log(gameObject.name + " focus");
            if (OnHover != null) OnHover();
        }
    
        inFocus = true;


    }
    void Select()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
           
            return;
        }

        if (!HumanInput()) return;
        if (OnSelect != null) OnSelect(); 
    }


    void OnMouseExit()
    {
       
       
        UnsetFocus();

        if (!HumanInput())
            return;
        if (OnHoverEnd != null) OnHoverEnd();
       
    }

    public void UnsetFocus()
    {
        inFocus = false;
    }

    bool HumanInput()
    {
        return TurnSystem.Instance == null || TurnSystem.Instance.Current == null || TurnSystem.Instance.Current.GetTurnControllerID() == 0 || Cheat;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Application.isEditor && Input.GetKeyUp(KeyCode.Alpha0))
            {
                Cheat = !Cheat;
            }
            if (!HumanInput())
                return;

            if (inFocus && !RC_Camera.CameraActionInProgress() )
            {
                Select();
            }            

        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log(gameObject.name+" focus "+inFocus);
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            Cheat = !Cheat;
        }
        if (!HumanInput()) return;

        if (inFocus && !RC_Camera.HasBlockingAction())
        {
            Select();
        } else
        {
            Debug.Log("asdsd");
        }
    }
}
