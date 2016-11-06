using UnityEngine;
using System.Collections;

public class UI_ToolTip_Attack : UI_ToolTip_AbilityBase
{
    public UI_EffectListView RegularList;
    public UI_EffectListView EffectList;
    public Transform AdrenalineBonus;
    public GameObject Divider;

    public override void SetAbility(UnitActionBase ability)
    {
        UnitAction_ApplyEffectFromWeapon ae = ability as UnitAction_ApplyEffectFromWeapon;

        
        RegularList.SetEffects(ae.GetRegularEffects());
        
        if (ae.WeaponBehaviorIndex == 0)
        {
            UI_AdrenalineRushBase adr_ui = (Instantiate(Resources.Load("UI/ui_adrenaline_rush")) as GameObject).GetComponent<UI_AdrenalineRushBase>();
            adr_ui.transform.SetParent(AdrenalineBonus, false);
            adr_ui.Init(ability.GetOwner().Stats);
        } else
        {
            Divider.SetActive(false);
        }
        
        base.SetAbility(ability);
    }
 
}
