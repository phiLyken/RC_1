using UnityEngine;
using System.Collections;
using System;

public class UnitRotationController : MonoBehaviour {


    public Transform m_rotated;

    public void Init(WaypointMover move, Func<Vector3> getRotationTarget )
    {
        m_rotated = move.transform;
     //   Debug.Log(" target");
        move.OnMoveToWayPoint += wp =>
        {
       //     Debug.Log(" Start");
            StopAllCoroutines();
            StartCoroutine(TurnToWaypoint(wp));
        };

        move.OnMovementEnd += wp =>
        {
        //    Debug.Log("  stop");
            StopAllCoroutines();
            StartCoroutine(TurnToFinalPosition(getRotationTarget()));
        };

 
 
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
     //   Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, MyMath.RotateToYSnapped(m_rotated.transform.position, _target.transform.position, 45), 0.35f);
      //  Debug.Log("Rotated");
        if (callback != null)
            callback();
    }
    IEnumerator TurnToFinalPosition(Vector3 target)
    {
       // Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, MyMath.RotateToYSnapped(m_rotated.transform.position, (target), 45), 0.35f);
      //  Debug.Log("Rotated");
    }
    IEnumerator TurnToWaypoint(IWayPoint wp)
    {
      //  Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, MyMath.RotateToYFlat(m_rotated.transform.position, wp.GetPosition()), 0.35f);
      //  Debug.Log("Rotated");
    }
}
