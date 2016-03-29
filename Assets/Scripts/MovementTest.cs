using UnityEngine;
using System.Collections;

public class MovementTest : MonoBehaviour {
    public Tile Target;
    public float Speed;

    public bool Start;
    bool started;

    void Update()
    {
        if(Start && !started)
        {
            started = true;
            Tile start = TileManager.Instance.GetClosestTile(transform.position);
            WaypointMover m = GetComponent<WaypointMover>();
            m.OnMovementEnd += OnEnd;
            m.MoveToDestination(start, Target, Speed);
        }    
    }

    public void OnEnd(IWayPoint t)
    {
        Debug.Log("end on " + ((Tile)t).name);
        Start = false;
        started = false;
    }
    
}
