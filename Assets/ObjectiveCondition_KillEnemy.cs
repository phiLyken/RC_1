using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ObjectiveCondition_KillEnemy : ObjectiveCondition {

    public int Count;
    bool has_selected;
    int killed;

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
        Unit.OnUnitKilled += OnKilled;
    }

    void OnKilled(Unit u)
    {
        if( u.OwnerID == 1)
        {
            killed++;
        }

        if (killed >= Count)
            Complete();
    }


    public override void SetActive()
    {
        base.SetActive();

        Unit.GetAllUnitsOfOwner(0, true).First().Actions.OnActionSelected += action =>
        {
            if ((action as UnitAction_ApplyEffectFromWeapon) != null)
            {
                has_selected = true;
            }
        };

        StartCoroutine(DelayedHint());
    }

    IEnumerator DelayedHint()
    {
        yield return new WaitForSeconds(8f);
        UI_Prompt.MakePrompt(
                          FindObjectsOfType<UI_ActionBar_ButtonAnchor>().Where(btn => btn.ButtonID == ActionButtonID.attack).First().transform as RectTransform,
                          "Select the ATTACK ability. Then click on an enemy.", 2,
                          delegate {
                              return has_selected;
                          },
                       true);

    }

    void StopTimer()
    {
        has_selected = true;
        StopAllCoroutines();
    }

}
