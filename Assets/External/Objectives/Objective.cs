using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Objective : MonoBehaviour, ICompletable {

    public bool InitOnStart;
    public ObjectiveConfig Config;        
    public event System.Action OnCancel;
    public event System.Action OnComplete;

    ObjectiveCondition Condition;  
     
    ObjectiveSetup Setup;
   
    public bool GetComplete()
    {
        return Condition.GetComplete();
    }

    public Objective Init (ObjectiveConfig config, Func<Objective, bool> canComplete)
    {
 
        Config = config;

        if(config.Setup != null)
            Setup = config.Setup.Instantiate(null, false).GetComponent<ObjectiveSetup>().Init();
   

        Debug.Log(Config.Description+" - "+config.Condition.GetType());

        Condition = Instantiate(config.Condition).GetComponent<ObjectiveCondition>();
        Condition.Init( delegate
            {
                return canComplete(this);
            }
        );

        Condition.OnComplete += Complete;
        Condition.OnCancel += Cancel;
        return this;
    }

    void Cancel()
    {
        OnCancel.AttemptCall();
    }

    void Complete()
    {
        OnComplete.AttemptCall();
        if(Setup != null)
            Setup.Remove();
    }

    public void Reset()
    {
        Condition.Reset();
    }

    void Start()
    {
        if (InitOnStart)
        {
            Init(Config, CanComplete);
        }
    }

        bool CanComplete(Objective obj){
            return true;
        }
}


