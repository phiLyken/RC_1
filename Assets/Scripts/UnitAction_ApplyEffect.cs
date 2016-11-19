using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_ApplyEffect : UnitActionBase
{
    public List<UnitEffect> Effects;
    public TargetInfo TargetRules;

 
    public override void SelectAction()
    { 
        base.SelectAction();

        if (OnTargetsFound != null)
        {
            OnTargetsFound( GetTargetableUnits().Select(u => u.gameObject).ToList()  );
        }

        if (Unit.HoveredUnit != null)
            OnUnitHover(Unit.HoveredUnit);

        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitHoverEnd += OnUnitUnhover;
        Unit.OnUnitSelect += OnUnitSelect;
    }
    public override void SetOwner(Unit o)
    {
        base.SetOwner(o);

        foreach (var e in GetEffects())
            e.Init(o);
    }
    public virtual float GetRange()
    {
        return GetTargetRules().GetRange(Owner);
    }

    protected virtual TargetInfo GetTargetRules()
    {
        return TargetRules;
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
        if(GetTargetableUnits().Contains(u) && OnTargetHover != null)
        {
          //  Debug.Log("Hovered unit on select");
            OnTargetHover(u);
        }
    }

    void OnUnitUnhover(Unit u)
    {
        if (GetTargetableUnits().Contains(u) && OnTargetUnhover != null)
        {
            OnTargetUnhover(u);
        }       
    }

    void OnUnitSelect(Unit u)
    {
        if ( !GetTargetableUnits().Contains(u))
        {
            ToastNotification.SetToastMessage1("Can't target this unit.");
            return;
        }
        AttemptAction(u);
    }

     

    public override void UnSelectAction()
    {
        Unit.OnUnitHover -= OnUnitHover;
        Unit.OnUnitHoverEnd -= OnUnitUnhover;
        Unit.OnUnitSelect -= OnUnitSelect;
        base.UnSelectAction();
    }

    protected override void ActionExecuted (Component target)
    {    
        
       StartCoroutine( ApplyEffects( Owner, ( target  as Unit) ) );
       target = null;
        
    }


    IEnumerator ApplyEffects(Unit atk, Unit target)
    {
        ActionInProgress = true;
        List<UnitEffect> Effects = GetEffects();

        Debug.Log("Applying " + Effects.Count + "effects   To " + target.GetID() );
        
 
 

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

        StartCoroutine(DelayedCompletion(0.5f));

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
    public List<Unit> GetTargetableUnits( )
    {
        List<Unit> targets = new List<Unit>(Unit.AllUnits);

        targets.RemoveAll(
            unit => !CanTarget(unit)    
        
        );

        return targets;
    }

    public bool CanTarget( Unit target, Tile fromTile)
    {
        return  TargetInfo.CanTarget(GetTargetRules(), Owner, target, fromTile);

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
