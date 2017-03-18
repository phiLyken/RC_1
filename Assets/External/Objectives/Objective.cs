using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Objective : MonoBehaviour, ICompletable {

    public bool InitOnStart;
    public ObjectiveConfig Config;        
    public event Action OnCancel;
    public event Action OnComplete;

    private int mIndex;
    public List<ObjectiveConfig> OtherObjectivesToComplete;

    ObjectiveCondition Condition;  
     
    List<ObjectiveSetup> Setups;
 

    public bool GetComplete()
    {
        return Condition.GetComplete();
    }

    public int GetIndex()
    {
        return mIndex;
    }
 
    public Objective Init (ObjectiveConfig config, Func<Objective, bool> canComplete, int index)
    {
        mIndex = index;
        Config = config;       

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

    private void SpawnSetups()
    {
        if (AllowedToComplete(this) && Setups ==null && !Config.Setup.IsNullOrEmpty())
        {
          
            MDebug.Log("SPAWN SETUP");
            Setups = M_Math.SpawnFromList(Config.Setup);
            Setups.ForEach(setup => setup.Init());
        }
    }
    void Cancel()
    {
        OnCancel.AttemptCall();
        if(Setups!= null)
        {
            Setups.ForEach(setup => setup.Remove());
        }
    }

    void Complete()
    {
        OnComplete.AttemptCall();
        if (Setups != null)
        {
            Setups.ForEach(setup => setup.Remove());
        }
    }

    public void Reset()
    {
        Condition.Reset();
    }

    void Start()
    {
        if (InitOnStart)
        {
            Init(Config, delegate
            { return true; }, -1);
        }
    }

    bool AllowedToComplete(Objective obj){
        return Condition.AllowedToComplete();
    }



    public bool GetShouldSave()
    {
        return Condition.GetShouldSave();
    }

    public string GetSaveID()
    {
        return Condition.GetSaveID();
    }

    public void SaveCompleted(bool b)
    {
        Condition.SaveCompleted(b);
    }

    public void SetActive()
    {
        Condition.SetActive();
        SpawnSetups();
        OnActivate();
    }
    
    protected virtual void OnActivate()
    { }

    public bool GetHasCompletedInSave()
    {
        return Condition.GetHasCompletedInSave();
    }
}


