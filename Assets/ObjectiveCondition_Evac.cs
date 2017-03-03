using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ObjectiveCondition_Evac : ObjectiveCondition {

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
    }

    public override void SetActive()
    {
        Unit.OnEvacuated += OnEvac;
        StartCoroutine(DelayedHint());

    }

    IEnumerator DelayedHint()
    {
        yield return new WaitForSeconds(3f);
        UI_Prompt.MakePrompt(
                          FindObjectsOfType<UI_ActionBar_ButtonAnchor>().Where(btn => btn.ButtonID == ActionButtonID.evac).First().transform as RectTransform,
                          "Evacuate Units and secure valuable Supplies", 2,
                          delegate {
                              return (Unit.GetAllUnitsOfOwner(0, true).IsNullOrEmpty());
                          },
                       true);

    }

    void OnEvac(Unit u)
    {
        Unit.OnEvacuated -= OnEvac;
        Complete();
    }
}
