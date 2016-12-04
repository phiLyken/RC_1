using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void WaypointEvent(IWayPoint p);

public class WaypointMover : MonoBehaviour {

    [HideInInspector]
    public bool Moving;

    public WaypointEvent OnWayPointreached;
	public WaypointEvent OnMoveToWayPoint;
    public WaypointEvent OnMovementEnd;
	public AnimationCurve MoveCurve;    

	public bool Loop;
	public float RandomDestination;
 

    public IWayPoint test_start;
    public IWayPoint test_end;

    
    
    public void MoveToDestination(IWayPoint start_waypoint, IWayPoint target_waypoint, float speed)
    {
        StopAllCoroutines();
        List<IWayPoint> waypoints = TileManager.Instance.FindPath( (Tile) start_waypoint, (Tile)target_waypoint, null).Cast<IWayPoint>().ToList();
        StartCoroutine(PatrolWaypoints( CreateWaypoints(waypoints, speed)));
    }


    public List<WaypointInfo> CreateWaypoints(List<IWayPoint> path, float speed)
    {
        Vector3 lastPos = transform.position;
        List<WaypointInfo> waypoints = new List<WaypointInfo>();

        foreach (IWayPoint wp in path)
        {
			float distance = Vector3.Magnitude(lastPos - wp.GetPosition());
            if (distance == 0)
            {
                continue;
            }
			waypoints.Add(new WaypointInfo(wp, distance / speed, MoveCurve));
			lastPos = wp.GetPosition();
        }
        return waypoints;
    }

    public void MoveOnPath(List<Tile> tiles, float speed)
    {
        StopAllCoroutines();
        List<IWayPoint> waypoints = tiles.Cast<IWayPoint>().ToList();
        StartCoroutine(PatrolWaypoints(CreateWaypoints(waypoints, speed)));
    }

	public void MoveOnPath(List<Transform> transforms, float speed)
	{
		StopAllCoroutines();
		List<IWayPoint> waypoints = transforms.Select( tr => new TransformWaypoint(tr)).Cast<IWayPoint>().ToList();

		StartCoroutine(PatrolWaypoints(CreateWaypoints(waypoints, speed)));
	}


	IEnumerator PatrolWaypoints(List<WaypointInfo> CurrentMoveWaypoints)
    {

       // Debug.Log(CurrentMoveWaypoints.Count);
		int currentIndex = 0;
        Moving = true;
        while (currentIndex < CurrentMoveWaypoints.Count) {
			
			if(OnMoveToWayPoint != null){
				OnMoveToWayPoint(CurrentMoveWaypoints[currentIndex].Waypoint);
			}
			yield return StartCoroutine(MoveToWaypoint(CurrentMoveWaypoints[currentIndex]));
            if (OnWayPointreached != null)
            {
                OnWayPointreached(CurrentMoveWaypoints[currentIndex].Waypoint);
            }
			currentIndex ++;
			if(Loop){
				currentIndex = currentIndex%CurrentMoveWaypoints.Count;
			}
			 
		}
        Moving = false;
        if (OnMovementEnd != null) OnMovementEnd(CurrentMoveWaypoints[currentIndex-1].Waypoint);
	}

	IEnumerator MoveToWaypoint(WaypointInfo waypoint){
		float t = Time.fixedDeltaTime;
		float startTime = Time.time - t;
		Vector3 startPos = transform.position;
		Vector3 targetPos = waypoint.Waypoint.GetPosition();
		
      
        while (t < 1) {			
			
			t = (Time.time - startTime) / waypoint.TimeToWaypoint;
			transform.position = Vector3.Lerp(startPos, targetPos,   waypoint.MovementCurve.Evaluate(t));			
			yield return new WaitForFixedUpdate();			
		}
       

	}	

}

public interface IWayPoint
{
    Vector3 GetPosition();
}

public class Waypoint : IWayPoint {

	Vector3 Position;

	public Vector3 GetPosition(){
		return Position;
	}

	public Waypoint(Vector3 pos){
		Position = pos;
	}
}

public class TransformWaypoint : IWayPoint {

	Transform tr;

	public Vector3 GetPosition(){
		return tr.position;
	}

	public TransformWaypoint(Transform _tr){
		tr = _tr;
	}
}

[System.Serializable]
public class WaypointInfo
{
    public AnimationCurve MovementCurve;
    public float TimeToWaypoint;
    public IWayPoint Waypoint;

    public WaypointInfo(IWayPoint p, float t, AnimationCurve c)
    {
        Waypoint = p;
        TimeToWaypoint = t;
        MovementCurve = c;
    }
}