using UnityEngine;
using System.Collections;

public class UI_ToolTip_Attack : UI_ToolTip_AbilityBase
{
    public UI_EffectListView RegularList;
    public UI_EffectListView EffectList;

    public GameObject Divider;

    public override void SetAbility(UnitActionBase ability)
    {
        UnitAction_ApplyEffectFromWeapon ae = ability as UnitAction_ApplyEffectFromWeapon;

        RegularList.SetEffects(ae.GetRegularEffects());

        if (ae.GetIntBonus() != null)
        {
            EffectList.SetEffects(MyMath.GetListFromObject(ae.GetIntBonus()));
        }      else
        {
            Divider.SetActive(false);
        }

       
      

        base.SetAbility(ability);
    }
 
}
