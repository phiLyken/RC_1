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
        if (!HumanInput()) return;
        if (inFocus && !PanCamera.CameraAction && Input.GetButtonUp("Fire1"))
        {
            Select();
        }
    }
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
        return TurnSystem.Instance.Current.GetTurnControllerID() == 0 || Input.GetKey(KeyCode.T);
    }

}
