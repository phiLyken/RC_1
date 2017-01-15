using UnityEngine;
using System.Collections;
using System;

public class CameraAction_Zoom : CameraAction {
    

    public float ScrollSpeed;
    float current_distance;
    public float ZoomSensitivity;
    public float MaxHeight;
    public float MinHeight;
 
    protected override bool CanStartInput()
    {
        return base.CanStartInput() && HasZoomInput();
    }

     protected  IEnumerator DoAction()
    {
        
        // Debug.Log("!camera startZoom");
        float StartTouchDistance = Input.GetAxis("Mouse ScrollWheel") != 0 ? 0 : (Input.touches[0].position - Input.touches[1].position).magnitude;
        float LastTouchDistance = StartTouchDistance;
        float CurrentDeltaDistance = 0;
        float CurrentTouchDistance = 0;

        Active = true;

        while (CanStartInput())
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                CurrentTouchDistance = Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity / Time.deltaTime;
            }
            else
            {
                CurrentTouchDistance = (Input.touches[0].position - Input.touches[1].position).magnitude / ZoomSensitivity;
            }

            CurrentDeltaDistance = (CurrentTouchDistance - LastTouchDistance);

            LastTouchDistance = CurrentTouchDistance;

            SetTargetDistance(CurrentTouchDistance );
            yield return null;
        }

        StartCoroutine(M_Math.DelayForFrames(5));
        Active = false;
 
        //Debug.Log("!camera endzoom");
      
       
        yield break;
    }

    void SetTargetDistance(float dist)
    {
        current_distance += dist;

    }
 
    bool HasZoomInput()
    {
        return (Input.touchCount > 1 || Input.GetAxis("Mouse ScrollWheel") != 0);
    }
 
    void Update()
    {
        if (!Active && CanStartInput())
        {
            StartCoroutine("DoAction");
        }

        MoveToDist();
     
    }

    void MoveToDist()
    {
        if (current_distance == 0)
            return;

        float move_delta = Time.deltaTime * ScrollSpeed * current_distance;

        if(current_distance > 0)
        {
            current_distance = Mathf.Max(0, current_distance - move_delta);
        } 
        
        if(current_distance < 0)
        {
            current_distance = Mathf.Min(0, current_distance - move_delta);
        }

        

        if(move_delta != 0)
        { 
            Vector3 newPos = transform.transform.position + (transform.transform.forward * move_delta);
            if (newPos.y > MinHeight && newPos.y < MaxHeight)
            {
                transform.transform.position = newPos;
            }

        }

    }

    float CameraDistanceToPlane()
    {
        return (M_Math.GetPlaneIntersectionY(new Ray(transform.position, transform.forward)) - transform.position).magnitude;
    }

    public override void Stop()
    {
        StopCoroutine("DoAction");
        current_distance = 0;
        Active = false;
    }
}
