using UnityEngine;
using System.Collections;
using System;

public class WaitForRotation : CustomYieldInstruction
{
    Transform Tr;
    Quaternion From;
    Quaternion To;
    float endTime;
    float totalTime;
    static int instance;

    int _instance;
    public WaitForRotation(Transform tr, Quaternion to, float time)
    {
        _instance = instance;
        instance++;
        Tr = tr;
        To = to;
        From = tr.rotation;
        // tr.rotation  = Quaternion.RotateTowards(tr.rotation, To, rad);
        totalTime = time;
        endTime = Time.time + time;

      //  Debug.Log(totalTime + " " + endTime);
    }

    float GetT()
    {
        return Mathf.Clamp01( 1 - (endTime - Time.time) / totalTime);
    }
    public override bool keepWaiting
    {
        get {
            
            Tr.rotation = Quaternion.Slerp(From, To, GetT());
         //   Debug.Log(_instance + " "+GetT());
            if (GetT() == 1)
            {
                Tr.rotation = To;
            }

            return GetT() < 1;
        }
    }
}
