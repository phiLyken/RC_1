using UnityEngine;
using System.Collections;
using System;

public class CameraAction_Rotate : CameraAction {
     
 
    public   float rotation_per_step;

 
    
    public void Rotate(int direction)
    {
       
        StartCoroutine( Rotate(direction * rotation_per_step));
    }

    IEnumerator Rotate(float delta)
    { 
        float rotated = 0;
        float t = 0;
        Active = true;
        while (Mathf.Abs(rotated) < Mathf.Abs(delta))
        {

            float target_rot = Mathf.Lerp(0, delta, t);
            float dr = target_rot - rotated;
            rotated += dr;
            t += Time.deltaTime * 3;
            transform.RotateAround(M_Math.GetCameraCenter(), Vector3.up, dr);
            yield return null;
        }

        transform.RotateAround(M_Math.GetCameraCenter(), Vector3.up, rotated - delta);

        Active = false;
         
    }
    public override void Stop()
    {
        Active = false;
    }
    void Update()
    {
        if (Active || !CanStartInput())
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(-1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(1);
        }
    }
}
