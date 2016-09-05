using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_ApplyEffect : UnitActionBase
{

    public List<UnitEffect> Effects;

    public TargetInfo TargetRules;

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
        return TargetRules.GetRange(Owner);
    }

    protected virtual List<UnitEffect> GetEffects()
    {
        if(Effects == null)
        {
            Debug.LogError("NO EFFECTS FOUND "+gameObject.name);
        }
        return Effects;
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



    public bool CanTarget( Unit target, Tile fromTile)
    {
        return  TargetInfo.CanTarget(TargetRules, Owner, target, fromTile);

    }

    /// <summary>
    /// Returns true if the effect can be applied applied from the owners tile [application filter & LOS & range are applied)
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool CanTarget(Unit target)
    {        
        return CanTarget( target, Owner.currentTile);
    }




}
