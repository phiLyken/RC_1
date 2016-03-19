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

        if(inFocus && !PanCamera.CameraAction && Input.GetButtonUp("Fire1"))
        {
            Select();
        }
    }
    void OnMouseOver()
    {
     
        if (!inFocus)
        {
            if (OnHover != null) OnHover();
        }
        inFocus = true;
       
      
    }
    void Select()
    {

        if (OnSelect != null) OnSelect(); 
    }


    void OnMouseExit()
    {
        
        inFocus = false;
        if (OnHoverEnd != null) OnHoverEnd();
       
    }

}
