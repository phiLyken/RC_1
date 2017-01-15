using UnityEngine;
using System.Collections;
using System;

public class CameraAction_Pan : CameraAction
{ 
    Vector3 smoothMove;
    public float PanThreshold;
    public float SmoothDrag;

    public int frames_delay = 5;
    bool _waitingForInput;
 
    protected override bool CanStartInput()
    {        
        return base.CanStartInput() && (Input.touchCount == 1 || Input.GetMouseButton(0));
    }

    

    IEnumerator DoPan()
    {
        Active = true;
        Vector3 startDragPos = M_Math.GetInputPos();
        Debug.Log("^cameraDOPAN");
        while (CanStartInput())
        {
            Vector3 mousePos = M_Math.GetInputPos();
            Vector3 delta = mousePos - startDragPos;
            Debug.DrawRay(startDragPos, delta, Color.green);

            smoothMove = -  Vector3.ClampMagnitude(delta * Time.fixedDeltaTime * 10, delta.magnitude);
           
            AttemptMove(smoothMove);
            yield return null;
        }

        yield return StartCoroutine(M_Math.DelayForFrames(frames_delay));
        Active = false;
       
        yield break;
    }
    void OnDrawGizmos()
    {
        if (Bounds  != null)
        {
            Gizmos.DrawWireCube(Bounds.Bounds().center, Bounds.Bounds().size);
        }       
    }

    IEnumerator WaitForInput()
    {
        _waitingForInput = true;
        Vector3 startDragPos = M_Math.GetInputPos();

       // Debug.Log("Wait for input");

        while(CanStartInput() && (startDragPos - M_Math.GetInputPos()).magnitude < PanThreshold)
        {
            yield return null;
        }
       
        if (CanStartInput())
        {
            StartCoroutine("DoPan");
        }
        _waitingForInput = false;
    }

    void Update()
    {
        if(!_waitingForInput &&  !Active &&  CanStartInput())
        {
            Stop();
            StartCoroutine(WaitForInput());
        } else if(!Active && !Mathf.Approximately(smoothMove.magnitude, 0))
        {
            smoothMove = Vector3.Lerp(smoothMove, Vector3.zero, Time.deltaTime * SmoothDrag);
            AttemptMove(smoothMove);    

        }
    }

    public override void Stop()
    {
 
        _waitingForInput = false;
        StopAllCoroutines();
        Active = false;
        smoothMove = Vector3.zero;
    }

}
