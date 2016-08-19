using UnityEngine;
using System.Collections;

public class RotationControllerTest : MonoBehaviour {

    public WaypointMover target;

    void Start()
    {
        Debug.Log(" target");
        target.OnMoveToWayPoint += wp =>
        {
            Debug.Log(" Start");
            StopAllCoroutines();
            StartCoroutine(TurnToWaypoint(wp));
        };
    }


    IEnumerator TurnToWaypoint(IWayPoint wp)
    {
        Debug.Log("Turn to");
        yield return new WaitForRotation(target.transform, MyMath.RotateToYSnapped(target.transform.position, wp.GetPosition(), 45), 0.5f);
        Debug.Log("Rotated");
    }
}
