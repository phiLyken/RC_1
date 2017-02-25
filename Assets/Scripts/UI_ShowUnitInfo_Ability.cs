using UnityEngine;
using System.Collections;

public class UI_ShowUnitInfo_Ability : MonoBehaviour {

    public UI_ToolTipAbility_Header Header;
    public UI_EffectListView Effects;

    public void SetAttack( WeaponBehavior behavior) {

        Header.SetBehavior(behavior);
       // Effects.SetEffects(behavior.Effects);
    }
}
