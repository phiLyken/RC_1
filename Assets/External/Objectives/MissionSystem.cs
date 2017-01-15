using UnityEngine;
using System.Collections;
using System;

public class MissionSystem : ObjectiveController {

    public static event Action<Objective> OnNewMission
    {
        add
        {
            instance.OnNext += value;
        }
        remove
        {
            instance.OnNext -= value;
        }
    }

    public static event Action<Objective> OnCompleteMission
    {
        add
        {
            instance.OnComplete += value;
           
        }
        remove
        {
            instance.OnComplete -= value;
        }
    }

    static MissionSystem _instance;
      
    public static MissionSystem instance
    {
       get
        {
            return _instance != null ? _instance : getInstance() ;
        }
    }

    static MissionSystem getInstance()
    {
        _instance=  GameObject.FindObjectOfType<MissionSystem>();
        if(_instance == null)
        {
            Debug.LogWarning("NO MISSION SYSTEM INSTANCE FOUND");           
        }
        _instance.Init();
        return _instance;
    }

 

}
