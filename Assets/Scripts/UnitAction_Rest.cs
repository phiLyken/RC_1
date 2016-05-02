using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rest : UnitActionBase
{
    TurnableEventHandler UpdateCostPreview;

    void Awake()
    {
        orderID = 3;
    }

    public override void SelectAction()
    {
        base.SelectAction();
        StartCoroutine(WaitForConfirmation());
    }
    

    public override void UnSelectAction()
    {
        base.UnSelectAction();
        StopAllCoroutines(); 
    }

    IEnumerator WaitForConfirmation()
    {
        while (!Input.GetButtonUp("Jump")) {
            yield return null;
        }

        AttemptExection();

    }

    protected override void ActionExecuted()
    {
        PlayerUnitStats stats = Owner.Stats as PlayerUnitStats;

        if(stats != null) { 

            int to_heal = (int) (stats.Int * Constants.INT_TO_HEAL);
            stats.AddInt(-stats.Int, false);
            stats.AddWill(to_heal);

        }
        base.ActionExecuted();
        ActionCompleted();
    }

}
