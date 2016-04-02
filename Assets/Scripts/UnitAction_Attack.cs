using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Attack : UnitActionBase {

    GameObject AimIndicator;

    public int Range;
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
    }

    public override void UnSelectAction()
    {
        AimIndicator.SetActive(false);
        Unit.OnUnitHover -= OnUnitHover;
        Unit.OnUnitSelect -= UnitSelected;
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
       
        StartCoroutine(AttackSequence(Owner, currentTarget, DMG));
    }

    IEnumerator AttackSequence(Unit atk, Unit def, Damage dmg)
    {
        PanCamera.Instance.PanToPos(atk.currentTile.GetPosition());
        yield return new WaitForSeconds(0.5f);
        SetLazer.MakeLazer(0.5f, new List<Vector3> { atk.transform.position, def.transform.position }, Color.red);
       
        PanCamera.Instance.PanToPos(def.currentTile.GetPosition());
        yield return new WaitForSeconds(0.75f);
        def.ReceiveDamage(dmg);
        Instantiate(Resources.Load("simple_explosion"), def.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.25f);
        base.ActionExecuted();

    }
    void OnUnitHover(Unit unit)
    {
        if (!isInRange(unit) || !canTarget(unit)) return;

        AimIndicator.SetActive(true);
        currentTarget = unit;
        AimIndicator.transform.position = unit.currentTile.GetPosition();
    }


    bool isInRange(Unit other)
    {
        return (other.currentTile.GetPosition() - Owner.currentTile.GetPosition()).magnitude <= Range;
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

}
