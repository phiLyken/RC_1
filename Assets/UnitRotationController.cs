using UnityEngine;
using System.Collections;

public class UnitRotationController : MonoBehaviour {


    public Transform m_rotated;

    public void Init(WaypointMover move, ActionManager actions)
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
            StartCoroutine(TurnToFinalPosition());
        };

        if(actions != null)
         actions.OnTargetAction += OnTargetAction;
 
    }

    void OnTargetAction(UnitActionBase action, Transform target)
    {
        Debug.Log("Rotation to target");
        TurnToPosition(target, null);
    }
    public void TurnToPosition(Transform target, EventHandler callback)
    {
        StartCoroutine(TurnToTargetPositiom(target, callback));
    }

    IEnumerator TurnToTargetPositiom(Transform _target, EventHandler callback)
    {
     //   Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, MyMath.RotateToYSnapped(m_rotated.transform.position, _target.transform.position, 45), 0.35f);
      //  Debug.Log("Rotated");
        if (callback != null)
            callback();
    }
    IEnumerator TurnToFinalPosition()
    {
       // Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, MyMath.RotateToYSnapped(m_rotated.transform.position, (m_rotated.transform.position + m_rotated.transform.forward), 45), 0.35f);
      //  Debug.Log("Rotated");
    }
    IEnumerator TurnToWaypoint(IWayPoint wp)
    {
      //  Debug.Log("Turn to");
        yield return new WaitForRotation(m_rotated.transform, MyMath.RotateToYFlat(m_rotated.transform.position, wp.GetPosition()), 0.35f);
      //  Debug.Log("Rotated");
    }
}
