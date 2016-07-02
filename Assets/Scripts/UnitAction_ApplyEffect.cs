using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_ApplyEffect : UnitActionBase
{
    public float Range = 4;
    public List<UnitEffect_Container> Effects;

    List<Unit>  targets;

    void Awake()
    {
        orderID = 3;
    }
    
    public override void SelectAction()
    {
        targets = null;
        base.SelectAction();

        if (OnTargetsFound != null)
        {
            float range = GetRange();
            targets = GetTargetableUnits(Unit.GetAllUnitsOfOwner((Owner.OwnerID + 1) % 2, true), Owner, range);
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
        return Effects.Select(e => e.GetEffect()).ToList();
    }

    void OnUnitHover(Unit u)
    {
        targets = MyMath.GetListFromObject(u);

        if (OnTargetHover != null) OnTargetHover(targets);
    }

    void OnUnitUnhover(Unit u)
    {
        if (u != this.Owner) return;

        targets = null;
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

        Debug.Log("Applying "+ GetEffects().Count+ "effects   To " + targets.Count+" targets");

        StartCoroutine(ApplySequence( Owner, targets  ));
        targets = null;
        //Debug.Break();
      
    }

    IEnumerator ApplyEffect(Unit instigator, Unit target, UnitEffect effect)
    {
       
        if (PanCamera.Instance != null)
            PanCamera.Instance.PanToPos(instigator.currentTile.GetPosition());

        effect.SpawnEffect(instigator.transform, target);

        yield return new WaitForSeconds(0.5f);
   
        if (PanCamera.Instance != null)
            PanCamera.Instance.PanToPos(target.currentTile.GetPosition());
        yield return new WaitForSeconds(0.5f);
       
        Instantiate(Resources.Load("simple_explosion"), target.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.4f);
        effect.ApplyToUnit(target);
    }

    IEnumerator ApplySequence(Unit atk, List<Unit> targets)
    {
        ActionInProgress = true;
        List<UnitEffect> Effects = GetEffects();
        Debug.Log("Applying " + Effects.Count + "effects   To " + targets.Count + " targets");
        
        foreach(Unit Target in targets)
        {
            yield return new WaitForSeconds(1f);

            foreach (UnitEffect effect in Effects)
            {
                if (!Target.IsDead()) { 

                    yield return StartCoroutine(ApplyEffect(atk, Target, effect));
                }
            }
        }
      
        yield return new WaitForSeconds(0.25f);
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
    public static List<Unit> GetTargetableUnits(List<Unit> list, Unit attacker, float range)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Unit u = list[i];
            if (!isInRange(attacker, u, range)) list.Remove(u);

        }
        return list;
    }
    public static bool isInRange(Unit instigator, Unit target, float range)
    {
        return isInRange(instigator, target, range, instigator.currentTile);

    }
    public static bool isInRange(Unit instigator, Unit target, float range, Tile origin)
    {
        List<Tile> in_range = LOSCheck.GetTilesVisibleTileInRange(origin, (int)range);

        return in_range.Contains(target.currentTile);
    }


}
