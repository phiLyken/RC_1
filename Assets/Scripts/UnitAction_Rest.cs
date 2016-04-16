using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rest : UnitActionBase
{
    TurnableEventHandler UpdateCostPreview;

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

        //Get Current amount of int
        float current_int = Owner.Stats.GetStat(UnitStats.Stats.intensity).current;
        float heal_amount = current_int * Constants.INT_TO_HEAL;

        //Empties Intensity
        Owner.Stats.GetStat(UnitStats.Stats.intensity).ModifyStat(-current_int);

        Owner.Stats.GetStat(UnitStats.Stats.will).ModifyStat(heal_amount);

        Owner.UpdateUI();
        base.ActionExecuted();
        ActionCompleted();
    }

}
