using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rest : UnitActionBase
{
    public float Range = 1;

    Unit currentTarget;

    void Awake()
    {
        orderID = 3;
    }

    public override void SelectAction()
    {
        currentTarget = null;
        base.SelectAction();

        if (OnTargetsFound != null)
        {
            List<GameObject> targets = new List<GameObject>();
            targets.Add(Owner.gameObject);
            OnTargetsFound(targets);
        }
        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitHoverEnd += OnUnitUnhover;
        Unit.OnUnitSelect += OnUnitSelect; 
    }    

    void OnUnitHover(Unit u)
    {
        if(u != Owner)
        {
            return;
        }

        currentTarget = u;
        if (OnTargetHover != null) OnTargetHover(currentTarget);
    }

    void OnUnitUnhover(Unit u)
    {
        if (u != this.Owner) return;

        currentTarget = null;
        if (OnTargetHover != null) OnTargetUnhover(u);
    }

    void OnUnitSelect(Unit u)
    {  
        if(u == Owner)
            AttemptExection();
    }

    public override bool CanExecAction(bool displayToast)
    {
        return base.CanExecAction(displayToast);       
    }


    public override List<Tile> GetPreviewTiles()
    {
        List<Tile> t = new List<Tile>();
        t.Add(Owner.currentTile);
        return t;
    }
    public override void UnSelectAction()
    {
        Unit.OnUnitHover -= OnUnitHover;
        Unit.OnUnitHoverEnd -= OnUnitUnhover;
        Unit.OnUnitSelect -= OnUnitSelect;
        base.UnSelectAction();      
    }

    protected override void ActionExecuted()
    {    
        base.ActionExecuted();
       
        currentTarget = null;

        ActionCompleted();
    }

}
