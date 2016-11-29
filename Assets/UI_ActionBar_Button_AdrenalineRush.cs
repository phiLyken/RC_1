using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_ActionBar_Button_AdrenalineRush : UI_AdrenalineRushBase {

    public Text BonusNumberText;
    public Text AdrenalineText;

    public string Format;

    public  Image Frame;
    public List<Image> Solid_Fill;
    public List<Image> Highlight_Fill;

 
    protected override void UpdateBonus(float _bonus)
    {
      
        this.ExecuteDelayed(EnableDelay, () =>  UpdateUI(_bonus) );
        
    }


    void UpdateUI(float bonus)
    { 
        ColorSetting_AdrRush_Attack colors = bonus > 1 ? UI_ActionBar_Button_ColorSetting.GetInstance().ADR_Attack_Active : UI_ActionBar_Button_ColorSetting.GetInstance().ADR_Attack_InActive;

        BonusNumberText.text = GetIntBonusText(bonus);
        BonusNumberText.color = colors.BonusText;
        AdrenalineText.color = colors.TitleText;
        Frame.color = colors.Frame;

        Solid_Fill.ChangeTint(colors.Fill);
        Highlight_Fill.ChangeTint(colors.Icon);
    }

    string GetIntBonusText(float bonus )
    {        

        (bonus)  =  (bonus - 1) * 100;     

        return string.Format(Format, bonus.ToString("#;-#;0"));
    }

}
