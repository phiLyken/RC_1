﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_ApplyEffect : UnitActionBase
{
    public bool TargetFriendly;
    public bool TargetEnemies;
    public bool TargetSelf;

    public float Range = 4;
    public List<UnitEffect_Container> Effects;
       
    List<Unit>  targets;

    void Awake()
    {
        orderID = 3;
    }
    
    public override void SelectAction()
    {
        targets = Unit.HoveredUnit != null ? MyMath.GetListFromObject(Unit.HoveredUnit) : null;

        base.SelectAction();

        if (OnTargetsFound != null)
        {
            float range = GetRange();
            targets = GetTargetableUnits( Unit.AllUnits);
            OnTargetsFound(
               targets.Select(u => u.gameObject).ToList()
             );
        }

        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitHoverEnd += OnUnitUnhover;
        Unit.OnUnitSelect += OnUnitSelect;
    }
    
    public virtual float GetRange()
    {
        return Range;
    }

    protected virtual List<UnitEffect> GetEffects()
    {
        if(Effects == null)
        {
            Debug.LogError("NO EFFECTS FOUND " + ActionID);
        }
        return Effects.Select(e => e.GetEffect()).ToList();
    }

    void OnUnitHover(Unit u)
    {

        targets = MyMath.GetListFromObject(u);

        if (OnTargetHover != null) OnTargetHover(u);
    }

    void OnUnitUnhover(Unit u)
    {
        if (u != this.Owner) return;

        targets = null;
        if (OnTargetHover != null) OnTargetUnhover(u);
    }

    void OnUnitSelect(Unit u)
    {
        if (targets != null && targets.Count > 0 && !GetTargetableUnits(Unit.AllUnits).Contains(targets[0]))
        {
            ToastNotification.SetToastMessage1("Can't target this unit.");
            return;
        }
        AttemptExection();
    }

    public override bool CanExecAction(bool displayToast)
    {

        return base.CanExecAction(displayToast);
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
        
        StartCoroutine(ApplySequence( Owner, targets  ));
        targets = null;
        //Debug.Break();
      
    }


    IEnumerator ApplySequence(Unit atk, List<Unit> targets)
    {
        ActionInProgress = true;
        List<UnitEffect> Effects = GetEffects();

        Debug.Log("Applying " + Effects.Count + "effects   To " + targets.Count + " targets");
        
        foreach(Unit target in targets)
        {
            bool first = true;
            foreach (UnitEffect effect in Effects)
            {
                if(!first) yield return new WaitForSeconds(0.5f);
                first = false;
                if(effect == null)
                {
                    Debug.LogError("NO EFFECT..");
                }
                if (!effect.GetTarget(target, atk).IsDead()) {
                    yield return StartCoroutine(effect.ApplyEffectSequence(target, Owner));
                }
            }
        }
      
        yield return new WaitForSeconds(2.5f);

        ActionCompleted();

    }
    public static List<Tile> GetTargetableTiles(Tile t, int range)
    {
        return new LOSCheck(t, TileManager.Instance).GetTilesVisibleTileInRange((range));
    }

    public List<Tile> GetTargetableTilesForUnit()
    {
        return GetTargetableTilesForUnit(Owner.currentTile);
    }

    public override List<Tile> GetPreviewTiles()
    {

        return GetTargetableTilesForUnit();
    }

    public List<Tile> GetTargetableTilesForUnit(Tile source_tile)
    {
        return GetTargetableTiles(source_tile, (int) GetRange());
    }

    /// <summary>
    /// Removes non-attackable units from the passed list
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public List<Unit> GetTargetableUnits(List<Unit> list)
    {
        List<Unit> targets = new List<Unit>(list);

        targets.RemoveAll(
            unit => !CanTarget(unit)    
        
        );

        return targets;
    }

    bool CanTarget(Unit target)

    {
        if (target == Owner && !TargetSelf) return false;
        if (!TargetSelf && (!TargetFriendly && target.OwnerID == Owner.OwnerID)) return false;
        if (!TargetEnemies && target.OwnerID != Owner.OwnerID) return false;        
        if (!isInRange(Owner, target, GetRange())) return false;

        return true;
    }

    public static bool isInRange(Unit instigator, Unit target, float range)
    {
        return isInRangeAndVisible(instigator, target, range, instigator.currentTile);

    }
    public static bool isInRangeAndVisible(Unit instigator, Unit target, float range, Tile origin)
    {
        List<Tile> in_range = LOSCheck.GetTilesVisibleTileInRange(origin, (int)range);

        return in_range.Contains(target.currentTile);
    }


}
