using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rage : UnitActionBase
{
    public int IntensityGain;
    public float WillConsumeChance;

     float[] WillLoseChances;
     float[] IntensityGainChance;
        void Awake()
        {
            orderID = 11;
        }
    public float Range = 1;

    Unit currentTarget;



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
        if (u != Owner)
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
        AttemptExection();
    }

    public override bool CanExecAction(bool displayToast)
    {
        return base.CanExecAction(displayToast);
    }

    protected override void ActionCompleted()
    {
        new Heal().ApplyToUnit(currentTarget);
        currentTarget = null;
        base.ActionCompleted();

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

        (Owner.Stats as PlayerUnitStats).AddInt(IntensityGain, Random.value < WillConsumeChance);
        base.ActionExecuted();
        ActionCompleted();
    }

    public int IntensityGained()
    {
        int gained = 0;
        foreach (float f in IntensityGainChance)
        {
            gained += (f > Random.value ? 1 : 0);
           // Debug.Log(gained);
        }
        return gained;
    }

    bool GetLooseWillOnRage(int current_will)
    {
        return WillLoseChances[Mathf.Min(current_will-1,WillLoseChances.Length-1)] > Random.value;
    }


}
