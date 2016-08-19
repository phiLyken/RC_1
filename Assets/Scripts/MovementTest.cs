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




			target.OnMoveToWayPoint = wp => {
				Debug.Log("move to "+wp.GetPosition());
			};

			target.OnWayPointreached = wp => {
				Debug.Log("reached  "+wp.GetPosition());
			};

			target.OnMovementEnd = wp => {
				Debug.Log("moved to "+wp.GetPosition());
				target.OnMovementEnd = null;
				target.OnMoveToWayPoint = null;
				target.OnWayPointreached = null;
			};
			target.MoveOnPath(waypoints, Speed);

		}
	}

	
    void Update()
    {
       
	
		if(Input.GetButtonDown("Jump")){
			Run();
		}
    
    }




    
}
