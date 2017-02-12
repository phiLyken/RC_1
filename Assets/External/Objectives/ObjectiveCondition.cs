using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition : MonoBehaviour, ICompletable {


    public event Action OnCancel;
    public event Action OnComplete;
    public Func<bool> AllowedToComplete;
    bool allowed = false;

    public string SaveID;

    public bool shouldSave;

    public bool GetComplete()
    {
        return allowed;
    } 

    protected  void Complete()
    {

        allowed = AllowedToComplete();

        if (allowed)
        {
            SaveCompleted(true);
            OnComplete.AttemptCall();
            
        }

    }


    public void Reset()
    {
        allowed = false;

    }

    public virtual void Init(Func<bool> canComplete)
    {
        
        AllowedToComplete = canComplete;
    }
    
    public virtual void SetActive()
    { }


    public bool GetHasCompletedInSave()
    {
   
        return PlayerPrefs.HasKey(GetSaveID()) ? PlayerPrefs.GetInt(GetSaveID()) == 1 : false;
    }

    public string GetSaveID()
    {
        return SaveID;
    }




    public void SaveCompleted(bool b)
    {
        PlayerPrefs.SetInt(GetSaveID(), b ? 1 : 0);
    }

    public bool GetShouldSave()
    {
        return shouldSave;
    }
}
