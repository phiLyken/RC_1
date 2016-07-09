using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class UnitEffect_Heal : UnitEffect
{

    protected override IEnumerator ApplyEffect(Unit target, UnitEffect effect)
    {
        UnitEffect_Heal copy = new UnitEffect_Heal(this);
       if (target.GetComponent<Unit_EffectManager>().ApplyEffect(copy))
        {
            OnApplied(target);
            copy.Ticked();
        }
        yield return null;
    }
    public UnitEffect_Heal(UnitEffect_Heal origin) : base(origin)
    {   }

    public UnitEffect_Heal()
    {   }

    private void OnApplied(Unit u)
    {
        (u.Stats as PlayerUnitStats).Rest();
    }
    public override void SetPreview(UI_DmgPreview prev, Unit target)
    {
        prev.Icon.gameObject.SetActive(false);
        prev.MainTF.text = "RESTORE O²";

        prev.IconTF.text = ((target.Stats as PlayerUnitStats).Max - (target.Stats as PlayerUnitStats).Will).ToString();
    }

}
