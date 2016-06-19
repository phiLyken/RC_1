using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rest : UnitActionBase
{
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
      //  Debug.Log("end  comfirmation");
        StopAllCoroutines();
        base.UnSelectAction();
      
    }

    IEnumerator WaitForConfirmation()
    {
      //  Debug.Log("Wait for comfirmation");
        while (!Input.GetButtonUp("Jump")) {
           
            yield return null;
        }
       // Debug.Log("REST");
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
