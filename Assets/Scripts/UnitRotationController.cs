using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UnitRotationController : MonoBehaviour {

    List<EventHandler> handlers;

    public Transform m_rotated;

    public UnitRotationController Init(WaypointMover move, Func<Vector3> getRotationTarget )
    {
        handlers = new List<EventHandler>();
        m_rotated = move.transform;
     //   Debug.Log(" target");
        move.OnMoveToWayPoint += wp =>
        {
       //     Debug.Log(" Start");
            StopAllCoroutines();
            handlers.ForEach(h => h());
            handlers = new List<EventHandler>();
            StartCoroutine(TurnToWaypoint(wp));

        };

        move.OnMovementEnd += wp =>
        {
        //    Debug.Log("  stop");
            StopAllCoroutines();
            handlers.ForEach(h => h());
            handlers = new List<EventHandler>();
            StartCoroutine(TurnToFinalPosition(getRotationTarget()));
        };

        StartCoroutine(  TurnToFinalPosition(getRotationTarget()));
        return this;
    }

    public void TurnToPosition(Transform target)
    {
        TurnToPosition(target, null);
    }
    public void TurnToPosition(Transform target, EventHandler callback)
    {
        StartCoroutine(TurnToTargetPositiom(target, callback));
    }
     

    public  IEnumerator TurnToTargetPositiom(Transform _target, EventHandler callback)
    {
        //cache the callback so we can call it before stopping coroutine
        if(callback != null)
        {
            handlers.Add(callback);
        }
     //   Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, M_Math.RotateToYSnapped(m_rotated.transform.position, _target.transform.position, 45), 0.35f);
      //  Debug.Log("Rotated");
        if (callback != null)
        {
            callback();
            handlers.Remove(callback);
        }
        yield return null;
    }
    IEnumerator TurnToFinalPosition(Vector3 target)
    {
       // Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, M_Math.RotateToYSnapped(m_rotated.transform.position, (target), 45), 0.35f);
      //  Debug.Log("Rotated");
    }
    IEnumerator TurnToWaypoint(IWayPoint wp)
    {
      //  Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, M_Math.RotateToYFlat(m_rotated.transform.position, wp.GetPosition()), 0.35f);
      //  Debug.Log("Rotated");
    }

    void OnDisable()
    {
        Debug.Log("Foo");
    }
}
