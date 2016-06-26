using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void TargetEvent(Unit instigator, Unit target, IUnitEffect dmg);

public class UnitAction_Attack : UnitActionBase {

    public int IntChangeOnUse;
    public float ChanceForIntChangeTrigger;

    public float Range;
    public bool CanTargetOwn;
    public bool CanTargetSelf;

    public static event TargetEvent OnTarget;

    public Damage DMG;

    Unit currentTarget;

    void Awake()
    {
        orderID = 1;
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
        return GetTargetableTiles(source_tile, (int)Range);
    }

    public override void SelectAction()
    {
        base.SelectAction();

        if(OnTargetsFound != null)
        {
            OnTargetsFound(
                GetTargetableUnits(Unit.GetAllUnitsOfOwner((Owner.OwnerID + 1) % 2, true), Owner, Range).Select(u => u.gameObject).ToList()
             );
        }

        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitSelect += OnUnitSelected;
        Unit.OnUnitHoverEnd += OnUnitHoverEnd;

        if (Unit.HoveredUnit != null) OnUnitHover(Unit.HoveredUnit);
    }

    public override void UnSelectAction()
    {
        base.UnSelectAction();
  
        if (UI_DmgPreview.Instance != null)
        {
            UI_DmgPreview.Instance.Disable();
        }

        Unit.OnUnitHover -= OnUnitHover;
        Unit.OnUnitSelect -= OnUnitSelected;
        Unit.OnUnitHoverEnd -= OnUnitHoverEnd;
     
    }

    void OnUnitSelected(Unit u)
    {
        if(u == currentTarget)
        {
            AttemptExection();
        }
    }
    
    protected override void ActionExecuted()
    {   
        Damage damage_dealt = new Damage();
        int damage = DMG.GetDamage();
        int bonus = (int)GetBonusIntRange().Value();
        ///this is what will be passed to the target as receiving damage
        damage_dealt.amount = bonus + damage;
        damage_dealt.bonus_damage = bonus;
        damage_dealt.base_damge = damage;        

        StartCoroutine(AttackSequence(Owner, currentTarget, damage_dealt));

        if( (Owner.Stats as PlayerUnitStats != null) && Random.value < ChanceForIntChangeTrigger)
        {
            (Owner.Stats as PlayerUnitStats).AddInt(IntChangeOnUse, false);
        }

        base.ActionExecuted();
    }

    IEnumerator AttackSequence(Unit atk, Unit def, Damage dmg)
    {
        ActionInProgress = true;

        if(PanCamera.Instance != null)
         PanCamera.Instance.PanToPos(atk.currentTile.GetPosition());

        yield return new WaitForSeconds(0.5f);

        SetLazer.MakeLazer(0.5f, new List<Vector3> { atk.transform.position, def.transform.position }, Color.red);

        if (PanCamera.Instance != null)
            PanCamera.Instance.PanToPos(def.currentTile.GetPosition());

        yield return new WaitForSeconds(0.75f);

        Instantiate(Resources.Load("simple_explosion"), def.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.25f);

        DamageNotification.SpawnDamageNotification(def.transform, dmg);
        def.ReceiveDamage(dmg);
        yield return new WaitForSeconds(0.25f);
        ActionCompleted();

    }

    void OnUnitHoverEnd(Unit u)
    {
        UI_DmgPreview.Instance.Disable();
        if (OnTargetUnhover != null) OnTargetUnhover(u);
    }

    void OnUnitHover(Unit unit)
    {
        if (unit == null || !isInRange(this.Owner, unit, Range) || !canTarget(unit))
        {
            if (UI_DmgPreview.Instance != null)
            {
                UI_DmgPreview.Instance.Disable();
            }
            if (OnTargetUnhover != null) OnTargetUnhover(unit);
            return;
        }
        
        currentTarget = unit;

        DMG.bonus_range = GetBonusIntRange();
        if (OnTargetHover != null) OnTargetHover(unit);
        if (OnTarget != null) OnTarget(Owner, currentTarget, DMG);
    }

    MyMath.R_Range GetBonusIntRange()
    {       
        PlayerUnitStats stats = (Owner.Stats as PlayerUnitStats);

        if (stats != null)
        {
            if(stats.Int > 0)
                return new MyMath.R_Range(1, stats.Int+1);
        }
        return new MyMath.R_Range(0, 0) ;
    }
    public static bool isInRange(Unit attacker, Unit other, float range)
    {
        return isInRange(attacker, other, range, attacker.currentTile);
        
    }
    public static bool isInRange(Unit attacker, Unit other, float range, Tile origin)
    {
        List<Tile> in_range = LOSCheck.GetTilesVisibleTileInRange(origin, (int) range);

        return in_range.Contains(other.currentTile);
    }
   

    bool canTarget(Unit other)
    {
        if (other == Owner && !CanTargetSelf) return false;
        if (other.OwnerID == Owner.OwnerID && !CanTargetOwn) return false;
        
        return true;
    }

    void OnDestroy()
    {
        Unit.OnUnitHover -= OnUnitHover;
        Unit.OnUnitSelect -= OnUnitSelected;
    }

    /// <summary>
    /// Removes non-attackable units from the passed list
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<Unit> GetTargetableUnits(List<Unit> list, Unit attacker, float range)
    {
        for(int i = list.Count-1; i >= 0; i--)
        {
            Unit u = list[i];
            if (!isInRange(attacker, u, range)) list.Remove(u);

        }
        return list;
    }

    public static bool CanTargetFromTile(Unit target, Unit instigator, Tile t, float range)
    {
        return isInRange(instigator, target, range, t);
    }

}
