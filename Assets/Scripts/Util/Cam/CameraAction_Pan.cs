﻿using UnityEngine;
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
        Vector3 startDragPos = MyMath.GetInputPos();
        while (CanStartInput())
        {
            // Debug.Log(CanStartInput()+"  "+Time.frameCount);
            Vector3 mousePos = MyMath.GetInputPos();
            Vector3 delta = mousePos - startDragPos;
            smoothMove = Vector3.ClampMagnitude(delta * Time.fixedDeltaTime * 10, delta.magnitude);

            transform.position = transform.position - smoothMove;
            yield return null;
        }

        yield return StartCoroutine(MyMath.DelayForFrames(frames_delay));
        Active = false;
       
        yield break;
    }
 

    IEnumerator WaitForInput()
    {
        _waitingForInput = true;
        Vector3 startDragPos = MyMath.GetInputPos();

       // Debug.Log("Wait for input");

        while(CanStartInput() && (startDragPos - MyMath.GetInputPos()).magnitude < PanThreshold)
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
        } else if(!Mathf.Approximately(smoothMove.magnitude, 0))
        {
            transform.Translate(-smoothMove, Space.World);
            smoothMove = Vector3.Lerp(smoothMove, Vector3.zero, Time.deltaTime * SmoothDrag);
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