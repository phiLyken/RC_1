using UnityEngine;
using System.Collections;

public class GlobalUpdateDispater_Test : MonoBehaviour {

    

    void Start()
    {
        GlobalUpdateDispatcher.OnUpdate += OnUpdate;
    }

    void OnUpdate(float f)
    {
       // MDebug.Log(f);
    }


}
