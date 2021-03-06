﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MissionSystem : ObjectiveController {

    public static event Action OnInit;

    public static event Action<Objective> OnNewMission
    {
        add
        {
            Instance.OnNext += value;
        }
        remove
        {
            Instance.OnNext -= value;
        }
    }

    public static event Action<Objective> OnCompleteMission
    {
        add
        {
            Instance.OnComplete += value;
           
        }
        remove
        {
            Instance.OnComplete -= value;
        }
    }

    static MissionSystem _instance;
      
    public static MissionSystem Instance
    {
       get
        {
            return _instance != null ? _instance : getInstance() ;
        }
    }

     
    static MissionSystem getInstance()
    {
       
        MissionSystem found =  GameObject.FindObjectOfType<MissionSystem>();
        if(found == null)
        {
           // Debug.LogWarning("NO MISSION SYSTEM INSTANCE FOUND");           
        } else if(_instance == null)
        {
           
            found.Init();
        }
        return _instance;
    }

    public static bool HasCompletedGlobal(string id)
    {
        return getInstance() == null || _instance.HasCompleted(id);
    }

    public override void Init(List<ObjectiveConfig> _objectives)
    {
        if(_instance == null)
        {
           
            _instance = this;
            base.Init(_objectives);
            OnInit.AttemptCall();
        }
    }

    void OnDestroy()
    {
        OnInit = null;
        _instance = null;
    }

}
