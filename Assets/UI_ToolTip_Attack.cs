using UnityEngine;
using System.Collections;

public class UI_ToolTip_Attack : UI_ToolTip_AbilityBase
{
    public UI_EffectListView RegularList;
    public UI_EffectListView EffectList;

    public override void SetAbility(UnitActionBase ability)
    {
        UnitAction_ApplyEffectFromWeapon ae = ability as UnitAction_ApplyEffectFromWeapon;
             
        EffectList.SetEffects(MyMath.GetListFromObject(ae.GetIntBonus()));
        RegularList.SetEffects(ae.GetRegularEffects());

        base.SetAbility(ability);
    }
 
}
