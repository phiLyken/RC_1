using UnityEngine;
using System.Collections;
using System;

public class BoundsExpander : MonoBehaviour {

    public MyMath.PositionLock Clamp;

    public Vector3 Expansion;
    public Vector3 Offset;
    public Transform Target;

    public Bounds mBounds;

    void OnDrawGizmos()
    {
       // Gizmos.DrawWireCube( transform.position, transform.localScale);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube( transform.position, transform.localScale);
        Debug.DrawRay(transform.position, Vector3.up * 10, Color.cyan);
    }

    void Start()
    {
        new PropertyWatcher_Vector().Init(GetTargetRotation, Updated);
        new PropertyWatcher_Vector().Init(GetTargetSize, Updated);
        new PropertyWatcher_Vector().Init(GetTargetPosition, Updated);
    }

    Vector3 GetTargetRotation()
    {
        return Target.rotation.eulerAngles;
    }
    Vector3 GetTargetSize()
    {
        return Target.Bounds().size;
    }

    Vector3 GetTargetPosition()
    {
        return Target.position;
    }
 
    void Updated(Vector3 v)
    {
        Bounds newbounds = Target.Bounds();
        mBounds = Target.Bounds();
        //newbounds.size += Expansion;
       // newbounds.center += Offset;
        transform.SetTransformToBounds(newbounds);
     //   Clamp.Apply(transform);
    }


    



	
}
