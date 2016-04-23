using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rage : UnitActionBase
{
    
    void Awake()
    {
        orderID = 11;
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

}
