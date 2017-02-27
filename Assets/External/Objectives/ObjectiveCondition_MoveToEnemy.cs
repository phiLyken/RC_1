using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ObjectiveCondition_MoveToEnemy : ObjectiveCondition
{

    Unit target_enemy;
    Unit player;
    bool has_selected;

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
    }

    
    void Update()
    {
        if(target_enemy == null)
        {
            target_enemy = Unit.GetAllUnitsOfOwner(1, true).FirstOrDefault();

            if(target_enemy != null)
            {
                target_enemy.OnIdentify += OnClose;
                
            }
        }

        if (player == null)
        {
            player = Unit.GetAllUnitsOfOwner(0, true).FirstOrDefault();

            if (player != null)
            {
                player.Actions.OnActionSelected += act => { if( (act as UnitAction_Move) != null) StopTimer(); };

            }
        }
    }
    public override void SetActive()
    {
        base.SetActive();
        
        StartCoroutine(DelayedHint());
    }

    void OnClose(Unit u)
    {

        target_enemy.OnIdentify -= OnClose;

        StartCoroutine(M_Math.ExecuteDelayed(3f, () => ToastNotification.SetToastMessage1("A prisoner?! He must have escaped from the mines.")));
        StartCoroutine(M_Math.ExecuteDelayed(4f, Complete));

    }

    IEnumerator DelayedHint()
    {
        yield return new WaitForSeconds(8f);
        UI_Prompt.MakePrompt(
                          FindObjectsOfType<UI_ActionBar_ButtonAnchor>().Where(btn => btn.ButtonID == ActionButtonID.move).First().transform as RectTransform,
                          "Select the MOVE ability.\nThen click on a tile.", 2,
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
