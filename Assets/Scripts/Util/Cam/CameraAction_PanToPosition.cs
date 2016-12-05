using UnityEngine;
using System.Collections;
using System;

public class CameraAction_PanToPosition : CameraAction
{

    protected EventHandler event_callback;
    public float DistanceSpeedFalloffStart;
    public AnimationCurve SpeedFalloff;
    Vector3 TargetPosition;
    public float Speed;


    public void GoTo(Vector3 pos)
    {
        GoToPos(pos, null);
    }

    public void GoToPos(Vector3 pos, EventHandler cb)
    {        
        StartPan(pos, Speed, cb);
    }


    public void StartPan(Vector3 pos, float speed, EventHandler _cb)
    {
        Callback();
        event_callback = _cb;
        ResetCallback(this);
        TargetPosition = pos;
        Speed = speed;

        StartCoroutine("DoAction");
    }

     
    IEnumerator DoAction( )
    {
       
        TargetPosition.y = 0;
        Active = true;

        Vector3 delta = TargetPosition - MyMath.GetCameraCenter();

        Debug.Log("^cameraStart Panning "+TargetPosition.ToString());
        while (delta.magnitude > 0.1f)
        {
            delta = TargetPosition - MyMath.GetCameraCenter();
            float speed = Speed;

            if (delta.magnitude < DistanceSpeedFalloffStart)
            {
                speed *= SpeedFalloff.Evaluate((DistanceSpeedFalloffStart  - delta.magnitude) / DistanceSpeedFalloffStart);
            }
         
           
            transform.Translate(Vector3.ClampMagnitude(delta.normalized * speed * Time.deltaTime, delta.magnitude), Space.World);

           

            Debug.DrawLine(MyMath.GetCameraCenter(), TargetPosition, Color.red);
           
            yield return null;
        }
        Debug.Log("^cameraEndPanning " + TargetPosition.ToString());
        Active = false;
        Callback();
 
    }

    protected void Callback()
    {
        if (event_callback != null)
        {
            event_callback();
            event_callback = null;
        }
    }

    public override void Stop()
    {
       // Debug.Log("STOP PAN");
        StopAllCoroutines();
        Callback();
        Active = false;
    }
}
