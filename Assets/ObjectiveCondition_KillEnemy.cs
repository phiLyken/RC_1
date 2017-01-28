using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition_KillEnemy : ObjectiveCondition {

    public int Count;

    int killed;

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
        Unit.OnUnitKilled += OnKilled;
    }

    void OnKilled(Unit u)
    {
        if( u.OwnerID == 0)
        {
            killed++;
        }

        if (killed >= Count)
            Complete();
    }
}
