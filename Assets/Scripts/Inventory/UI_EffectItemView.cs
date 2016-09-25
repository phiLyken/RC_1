using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_EffectItemView : MonoBehaviour, IToolTip {
    public UnitEffect m_effect;

    public Sprite Target_Owner;
    public Sprite Target_Target;

    public Image Icon;
    public Image BG;

    public Text Text;

    public UnitEffect GetEffect()
    {
        return m_effect;
    }
    public void SetEffect(UnitEffect new_effeect)
    {

        BG.sprite = new_effeect.TargetMode == UnitEffect.TargetModes.Owner ? Target_Owner : Target_Target;

        m_effect = new_effeect;
        
        if(new_effeect.Icon != null)
             Icon.sprite = new_effeect.Icon;

        Text.text = new_effeect.GetShortHandle();

    }

    public object GetItem()
    {
        return GetEffect();
    }
}
