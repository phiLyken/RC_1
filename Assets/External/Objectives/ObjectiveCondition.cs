using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition : MonoBehaviour, ICompletable {

    public event System.Action OnCancel;
    public event System.Action OnComplete;
    Func<bool> CanComplete;
    bool ConditionMet = false;

    public bool GetComplete()
    {
        return ConditionMet;
    } 

    protected  void Complete()
    {

        ConditionMet = CanComplete();

        if(ConditionMet)
            OnComplete.AttemptCall();
 
    }

    public void Reset()
    {
        ConditionMet = false;

    }

    public virtual void Init(Func<bool> canComplete)
    {
        
        CanComplete = canComplete;
    }
    
 
}
