﻿using UnityEngine;
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

    public static   string ToString2()
    {
        string str = "";
        if(events == null ||events.Count  == 0)
        {
            return "none";
        }

        events.ForEach(ev => str += ev.EventID + ",");

        return str;
    }
    public static void Reset()
    {
        events = new List<TurnEvent>();
    }
        
    public class TurnEvent
    {
        public string EventID;
        protected object instigator;
        protected EventHandler callback;

        public virtual void StartEvent()
        {
            if (events == null)
                events = new List<TurnEvent>();

            //Debug.Log("Event Started");
            events.Add(this);
        }

        public virtual void EndEvent( )
        {
            events.Remove(this);
            //Debug.Log("Event Ended  remaining   "+events.Count);
 
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
            EventID = "CAMERA FOCUS";
            position = pos;
            StartEvent();
        }
    }

    public class AIAggroEvent : TurnEvent
    {
        UnitAI ai;
        Unit target;
        UnitRotationController rotator;
        UnitAnimationController animator;

        public override void StartEvent()
        {           
            TurnSystem.Instance.OnGlobalTurn += Execute;
        }

        public AIAggroEvent(UnitAI _ai, Unit _target)
        {
           
            ai = _ai;
            target = _target;
            rotator = ai.GetComponentInChildren<UnitRotationController>();
            animator = ai.GetComponentInChildren<UnitAnimationController>();
            Debug.Log("Turnevent Queue start aggro");
            rotator.TurnToPosition(target.transform);
        }

        void Execute(int i)
        {
            base.StartEvent();
            rotator.TurnToPosition(target.transform, () => animator.PlayAnimation(UnitAnimationTypes.bAggro, EndEvent));
            Debug.Log("Turnevent Queue execute aggro");
            TurnSystem.Instance.OnGlobalTurn -= Execute;

        }


    }
    
    


}
