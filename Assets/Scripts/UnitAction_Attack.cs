using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Attack : UnitActionBase {

    GameObject AimIndicator;
     
    public int IntUsageForAttack;
    public float IntToDamage = 0;
    public float Range;
    public bool CanTargetOwn;
    public bool CanTargetSelf;

    public Damage DMG;
    Unit currentTarget;


    void Start()
    {
        GameObject pref = Resources.Load("target_indicator") as GameObject;
        AimIndicator = Instantiate(pref);
        AimIndicator.SetActive(false);
    }
    public override void SelectAction()
    {
        base.SelectAction();
        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitSelect += UnitSelected;
        List<Tile> AttackableTiles = new LOSCheck(Owner.currentTile, TileManager.Instance).GetTilesVisibleTileInRange((int) Range);

        //  TileCollectionHighlight.SetHighlight(TileManager.Instance.GetTilesInDistance(Owner.currentTile.GetPosition(), Range), "attack_range");
        TileCollectionHighlight.SetHighlight(AttackableTiles, "attack_range");
        if (Unit.HoveredUnit != null) OnUnitHover(Unit.HoveredUnit);
    }

    public override void UnSelectAction()
    {
        AimIndicator.SetActive(false);
        Unit.OnUnitHover -= OnUnitHover;
        Unit.OnUnitSelect -= UnitSelected;
        TileCollectionHighlight.DisableHighlight();
    }

    void UnitSelected(Unit u)
    {
        if(u == currentTarget)
        {
            AttemptExection();
        }
    }
    
    protected override void ActionExecuted()
    {
        Owner.ModifyInt(-IntUsageForAttack);
        Damage newd = new Damage();
      
     
       
        newd.amount = (int)( DMG.amount * GetIntMod());

        newd.bonus_damage = newd.amount - DMG.amount;
        newd.base_damge = DMG.amount;
        

        StartCoroutine(AttackSequence(Owner, currentTarget, newd));
        base.ActionExecuted();
    }

    float GetIntMod()
    {
        return 1 + Owner.Stats.GetStat(UnitStats.Stats.intensity).current * IntToDamage;
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

        DamageNotification.Spawn(def.transform, dmg);
        def.ReceiveDamage(dmg);
        yield return new WaitForSeconds(0.25f);
        ActionCompleted();

    }
    void OnUnitHover(Unit unit)
    {
        if (!isInRange(this.Owner,unit,Range) || !canTarget(unit)) return;

        AimIndicator.SetActive(true);
        currentTarget = unit;
        AimIndicator.transform.position = unit.currentTile.GetPosition();
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
        Unit.OnUnitSelect -= UnitSelected;
    }

    /// <summary>
    /// Removes non-attackable units from the passed list
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<Unit> GetAttackableUnits(List<Unit> list, Unit attacker, float range)
    {
        for(int i = list.Count-1; i >= 0; i--)
        {
            Unit u = list[i];
            if (!isInRange(attacker, u, range)) list.Remove(u);

           
        }

        return list;
    }

    public static bool CanAttackFromTile(Unit target, Unit attacker, Tile t, float range)
    {
        return isInRange(attacker, target, range, t);
    }



}
