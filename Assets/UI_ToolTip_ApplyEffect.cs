using UnityEngine;
using System.Collections;

public class UI_ToolTip_ApplyEffect : UI_ToolTip_AbilityBase {
    
    public UI_EffectListView EffectList;


    public override void SetAbility(UnitActionBase ability)
    {
        base.SetAbility(ability);

        UnitAction_ApplyEffect ap = ability as UnitAction_ApplyEffect;
        Debug.Log(ap.Effects.Count);
        EffectList.SetEffects(ap.Effects);
    }
}
