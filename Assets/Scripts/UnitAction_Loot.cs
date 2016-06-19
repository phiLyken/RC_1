using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Loot : UnitActionBase
{
    void Awake()
    {
        orderID = 10;
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
    public override bool CanExecAction(bool b)
    {
        Tile_Loot l = Owner.currentTile.GetComponent<Tile_Loot>();

        if(l == null)
        {
            if(b)ToastNotification.SetToastMessage2("No Loot Nearby");
            return false;
        }

        return base.CanExecAction(b);
    }

    protected override void ActionExecuted()
    {
        base.ActionExecuted();
        Tile_Loot l = Owner.currentTile.GetComponent<Tile_Loot>();
        l.OnLoot(Owner);
    }

}
