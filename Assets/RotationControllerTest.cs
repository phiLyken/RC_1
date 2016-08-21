using UnityEngine;
using System.Collections;

public class RotationControllerTest : MonoBehaviour {

    public UnitRotationController rotator;
    public WaypointMover movement;
    void Start()
    {

        rotator.Init(movement);

        TileSelecter.OnTileSelect += t => { rotator.TurnToPosition(t.transform, CB); };
    }
   
    void CB()
    {
        Debug.Log(" turned");
    }

   
}
