using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_ToolTip_AdrenalineRush : UI_AdrenalineRushBase
{

    public Text BonusNumberText;
    public Text BonusDescrText;
    public Image BG;
    public Image Icon;

    public string Format;





    protected override void UpdateBonus(int _bonus)
    {

        this.ExecuteDelayed(EnableDelay, () => UpdateUI(_bonus));

    }


    void UpdateUI(int bonus)
    {
        ColorSettingToolTipItem colors = bonus > 1 ? UI_ActionBar_Button_ColorSetting.GetInstance().TT_ADR_Bonus_Active  : UI_ActionBar_Button_ColorSetting.GetInstance().TT_ADR_Bonus_Inactive;

        BonusNumberText.text = GetIntBonusText(bonus);
        BonusNumberText.color = colors.Text;
        BonusDescrText.color = colors.Text;

        BG.color = colors.Background;
        Icon.color = colors.Icon;
 
    }

    string GetIntBonusText(int _bonus)
    {
        (_bonus) = (_bonus - 1) * 100;


        return string.Format(Format, _bonus.ToString());
    }

}
