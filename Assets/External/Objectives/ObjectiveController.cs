using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ObjectiveController : MonoBehaviour {

   
    public bool InitOnStart;
    public bool CompleteAny;

    public int DebugStartAt;

    public bool ResetOnStart;

    public List<ObjectiveConfig> Configs;

    protected CompletionStack<Objective> Objectives;
    public event Action<Objective> OnComplete;
    public event Action<Objective> OnNext;

    void Start()
    {
        if(InitOnStart)
             Init(Configs);
    }

    public void Init()
    {
        Init(Configs);
    }

    public void SetSaves(int count)
    {
        count = Mathf.Min(count, Configs.Count);

        for (int i = 0; i < count; i++)
        {
            Configs[i].Condition.SaveCompleted(true);
        }
    }
    public void ResetSaves()
    {

        Configs.ForEach(conf => conf.Condition.SaveCompleted(false));
        
       
    }
    public virtual void Init(List<ObjectiveConfig> _objectives)
    {
        if (ResetOnStart)
            ResetSaves();

        SetSaves(DebugStartAt);

        List<Objective> objectives = new List<Objective>();
        GameObject newGO = new GameObject("objective ");
        newGO.transform.parent = this.transform;
        int index = 0;

        _objectives
            .Where(obj => !obj.Condition.GetHasCompletedInSave()).ToList()
            .ForEach(obj => {
               
                Objective objective = newGO.AddComponent<Objective>();
                objectives.Add(objective);
                objective.Init(obj, CanComplete, index);
                index++;
            });

      

        Objectives = new CompletionStack<Objective>(objectives, CompleteAny);

        if (CompleteAny)
        {
            Objectives.OnComplete += OnObjectiveComplete;
        }
        else
        {
            Objectives.OnCurrentComplete += OnObjectiveComplete;
        }

        Objectives.OnSetNew += OnSetNew;
        Objectives.Init();

       /// objectives.ForEach(objective => objective.SetActive());
    }



    void OnObjectiveComplete(Objective obj)
    {
        Debug.Log("Objective complete " + obj.Config.Title);
        OnComplete.AttemptCall(obj);
    }

    void OnSetNew(Objective obj)
    {
        Debug.Log("Objective new " + obj.Config.Title +" "+obj.Config.Condition.name);
        obj.SetActive();
        OnNext.AttemptCall(obj);
    }

    public List<Objective> GetObjectives()
    {
        return Objectives.GetItems();
    }

    public bool CanComplete(Objective obj)
    {
        return Objectives.CanComplete(obj);
    }

    public virtual bool HasCompleted(string missionID)
    {

        foreach(var objective in Objectives.GetItems())
        {
            if(objective.Config.ID == missionID) 
            {
                return objective.GetHasCompletedInSave();
            }
        }

        return true;
    }
}
