using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnEventQueue  {

    static List<TurnEvent> events;

    public static bool EventRunning
    {
        get
        {
            return events != null && events.HasItems();
        }

       
    }

  
    public static void Reset()
    {
        events = new List<TurnEvent>();
    }
        
    public class TurnEvent
    {
        protected object instigator;
        protected EventHandler callback;

        public virtual void StartEvent()
        {
            if (events == null)
                events = new List<TurnEvent>();

         //   Debug.Log("Event Started");
            events.Add(this);
        }

        public virtual void EndEvent( )
        {
            events.Remove(this);
         //   Debug.Log("Event Ended  remaining   "+events.Count);
 
            if(callback != null)
                callback();
        }

    }

    public class CameraFocusEvent : TurnEvent
    {
        Vector3 position;

        public override void StartEvent( )
        {
            base.StartEvent( );
            if(RC_Camera.Instance != null)
            {
                RC_Camera.Instance.ActionPanToPos.GoToPos(position, EndEvent);
            } else
            {
                EndEvent();
            }
        }     

        public CameraFocusEvent(Vector3 pos )
        {          
          
            position = pos;
            StartEvent();
        }
    }
    
    


}
