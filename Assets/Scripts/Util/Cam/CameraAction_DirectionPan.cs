using UnityEngine;
using System.Collections;
using System;

public class CameraAction_DirectionPan : CameraAction {

    public float Speed;

    public override void Stop()
    {
       
    }


    void Update()
    {
        if (CanStartInput())
        {
            
            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            Vector3 InputDir = new Vector3(x, 0, z);
            Vector3 AdjustedInputDir = transform.TransformDirection(InputDir);
            AdjustedInputDir.y = 0;
            AdjustedInputDir.Normalize();

            if(!Mathf.Approximately( AdjustedInputDir.magnitude, 0))
            {
                if (!Active)
                {
                    ResetCallback(this);
                }

                Active = true;
                transform.Translate(AdjustedInputDir * Time.deltaTime * Speed, Space.World);
                Debug.DrawRay(transform.position, AdjustedInputDir * 5, Color.red);
            } else
            {
                Active = false;
            }
        }
    }

 

}
