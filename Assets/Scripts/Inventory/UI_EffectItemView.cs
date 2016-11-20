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
    public void SetEffect(UnitEffect new_effect)
    {

        UI_ActionBar_Button_ColorSetting setting = UI_ActionBar_Button_ColorSetting.GetInstance();
        ColorSettingToolTipItem color = new_effect.EffectBonus > 1 ? setting.TT_EFF_Bonus_Active : setting.TT_EFF_Bonus_Inactive;

        BG.sprite = new_effect.TargetMode == UnitEffect.TargetModes.Owner ? Target_Owner : Target_Target;

        m_effect = new_effect;
       
       
        if(new_effect.Icon != null)
        {
            Icon.sprite = new_effect.Icon;
            Icon.color = color.Icon;
        }


        Text.text = new_effect.GetShortHandle();
        Text.color = color.Text;
 
        BG.color = color.Background;

    }

    public object GetItem()
    {
        return GetEffect();
    }
}
