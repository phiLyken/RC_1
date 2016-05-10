using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnEventQueue  {

    public static TurnEvent Current;
        
    public class TurnEvent
    {
        protected object instigator;
        protected EventHandler callback;

        public virtual void StartEvent()
        {

        }

        public virtual void EndEvent()
        {
            Current = null;
            Debug.Log("Event Ended");
            callback();
        }

    }

    public class CameraFocusEvent : TurnEvent
    {
        Vector3 position;

        public override void StartEvent()
        {
            if(PanCamera.Instance != null)
            {
                PanCamera.Instance.PanToPos(position, EndEvent);
            } else
            {
                EndEvent();
            }
        }     

        public CameraFocusEvent(Vector3 pos, EventHandler cb)
        {
            Current = this;
            Debug.Log("Camera Event");
            callback = cb;
            position = pos;

            StartEvent();
        }
    }
    
    


}
