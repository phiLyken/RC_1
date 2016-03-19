using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void WaypointEvent(IWayPoint p);

public class WaypointMover : MonoBehaviour {

    [HideInInspector]
    public bool Moving;

    public WaypointEvent OnWayPointreached;
    public WaypointEvent OnMovementEnd;
	public AnimationCurve MoveCurve;    

	public bool Loop;
	public float RandomDestination;
  
	
	float currentStartTime;

    public IWayPoint test_start;
    public IWayPoint test_end;

    
    
    public void MoveToDestination(IWayPoint start_waypoint, IWayPoint target_waypoint, float speed)
    {
        StopAllCoroutines();
        List<IWayPoint> waypoints = TileManager.Instance.FindPath( (Tile) start_waypoint, (Tile)target_waypoint).Cast<IWayPoint>().ToList();
        StartCoroutine(PatrolWaypoints( CreateWaypoints(waypoints, speed)));
    }


    public List<WaypointInfo> CreateWaypoints(List<IWayPoint> path, float speed)
    {
        Vector3 lastPos = transform.position;
        List<WaypointInfo> waypoints = new List<WaypointInfo>();

        foreach (Tile t in path)
        {
            float distance = Vector3.Magnitude(lastPos - t.GetPosition());
            if (distance == 0)
            {
                continue;
            }
            waypoints.Add(new WaypointInfo(t, distance / speed, MoveCurve));
            lastPos = t.GetPosition();
        }
        return waypoints;
    }

    public void MoveOnPath(List<Tile> tiles, float speed)
    {
        StopAllCoroutines();
        List<IWayPoint> waypoints = tiles.Cast<IWayPoint>().ToList();
        StartCoroutine(PatrolWaypoints(CreateWaypoints(waypoints, speed)));
    }
	
	IEnumerator PatrolWaypoints(List<WaypointInfo> CurrentMoveWaypoints)
    {

        Debug.Log(CurrentMoveWaypoints.Count);
		int currentIndex = 0;
        Moving = true;
        while (currentIndex < CurrentMoveWaypoints.Count) {
			
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
		Vector3 dLastPosition = transform.position;
      
        while (t < 1) {			
			dLastPosition = transform.position;			
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