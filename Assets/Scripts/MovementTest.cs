using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementTest : MonoBehaviour {

	public WaypointMover target;
	public List<Transform> waypoints;
    public float Speed;

 

	void Run(){
		
		if(!target.Moving){




            target.OnMoveToWayPoint += LogPos;


            target.OnWayPointreached += LogPos;

            target.OnMovementEnd += Ended;

			target.MoveOnPath(waypoints, Speed);

		}
	}

	void Ended(IWayPoint point)
    {
       
        target.OnMovementEnd -= Ended;
        target.OnMoveToWayPoint -= LogPos;
        target.OnWayPointreached -= LogPos;
    }
    void LogPos(IWayPoint wp)
    {
        Debug.Log(wp.GetPosition());
    }
    void Update()
    {
       
	
		if(Input.GetButtonDown("Jump")){
			Run();
		}
    
    }




    
}
