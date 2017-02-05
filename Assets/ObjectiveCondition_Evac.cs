using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition_Evac : ObjectiveCondition {

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
    }

    public override void SetActive()
    {
        Unit.OnEvacuated += OnEvac;
    }

    void OnEvac(Unit u)
    {
        Unit.OnEvacuated -= OnEvac;
        Complete();
    }
}
