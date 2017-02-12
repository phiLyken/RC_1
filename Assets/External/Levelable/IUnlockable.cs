using UnityEngine;
using System.Collections;
using System;

public interface IUnlockable  {

    string GetID();
    void AddUnlockListener(Action OnUnlock); 
    int GetLevelRequirement();
    void SetLevels(Levels levels);
    bool IsUnlocked();
 
}

 



