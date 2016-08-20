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

        target.OnMovementEnd += wp =>
        {
            Debug.Log(" stop");
            StopAllCoroutines();
            StartCoroutine(TurnToFinalPosition());
        };

        TileSelecter.OnTileSelect += t => { StartCoroutine(TurnToTargetPositiom(t.transform)); };
    }
    IEnumerator TurnToTargetPositiom(Transform _target)
    {
        Debug.Log("Turn to");
        yield return new WaitForRotation(target.transform, MyMath.RotateToYSnapped(target.transform.position, _target.transform.position, 45), 0.35f);
        Debug.Log("Rotated");
    }
    IEnumerator TurnToFinalPosition()
    {
        Debug.Log("Turn to");
        yield return new WaitForRotation(target.transform, MyMath.RotateToYSnapped(target.transform.position, ( target.transform.position + target.transform.forward), 45), 0.35f);
        Debug.Log("Rotated");
    }
    IEnumerator TurnToWaypoint(IWayPoint wp)
    {
        Debug.Log("Turn to");
        yield return new WaitForRotation(target.transform, MyMath.RotateToYFlat(target.transform.position, wp.GetPosition()), 0.35f);
        Debug.Log("Rotated");
    }

    void Update()
    {
      //  transform.rotation = MyMath.RotateToYSnapped(target.transform.position, target.transform.position, 45);
    }
}
