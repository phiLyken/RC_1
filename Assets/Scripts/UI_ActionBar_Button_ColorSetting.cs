using UnityEngine;
using System.Collections;

public class UI_ActionBar_Button_ColorSetting : ScriptableObject {

    public ColorSetting_Button BTN_Active;
    public ColorSetting_Button BTN_Inactive;
    public ColorSetting_Button BTN_ADR_Rush;
    public ColorSetting_Button BTN_Selected;

    public ColorSetting_AdrRush_Attack ADR_Attack_Active;
    public ColorSetting_AdrRush_Attack ADR_Attack_InActive;

    public static UI_ActionBar_Button_ColorSetting GetInstance()
    {

        return Resources.Load("UI/UI_ActionBar_Button_ColorSetting") as UI_ActionBar_Button_ColorSetting;
    }

 
}


[System.Serializable]
public class  ColorSetting_Button 
{
    public Color Frame = Color.magenta;
    public Color Icon = Color.magenta;

    public Color Charge_Text = Color.magenta;
    public Color Charge_Counter_Fill = Color.magenta;
    public Color Charge_Counter_Frame = Color.magenta;
    public Color Charge_CounterIcon = Color.magenta;  

}

[System.Serializable]
public class ColorSetting_AdrRush_Attack
{
    public Color Fill = Color.magenta;
    public Color BonusText = Color.magenta;
    public Color TitleText = Color.magenta;
    public Color Icon = Color.magenta;
    public Color Frame = Color.magenta;


}

