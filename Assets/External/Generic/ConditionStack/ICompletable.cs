using UnityEngine;
using System.Collections;
using System;

public interface ICompletable  {

    event System.Action OnComplete;
    event System.Action OnCancel;

    
    bool GetComplete();
    void Reset();
}

