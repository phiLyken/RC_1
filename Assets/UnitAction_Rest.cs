using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rest : UnitActionBase
{

    public override void SelectAction()
    {
        StartCoroutine(WaitForConfirmation());
    }
    
    protected override void ActionExecuted()
    {
      
    }

    public override void UnSelectAction()
    {
        StopAllCoroutines();
    }

    IEnumerator WaitForConfirmation()
    {
        while (!Input.GetKeyUp(KeyCode.Return)) {
            yield return null;
        }

        AttemptExection();

    }



}
