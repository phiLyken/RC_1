using UnityEngine;
using System.Collections;

public class UI_ShowEffectToolTip : MonoBehaviour
{
    public GameObject ToolTip;


    public void ShowItemTooTip()
    {
        SetItemToolTip(GetComponent<UI_EffectItemView>().GetEffect());
    }

    void SetItemToolTip(UnitEffect effect)
    {
        GameObject instance = null;
        Destroy(ToolTip);

        ToolTip = Instantiate(Resources.Load("UI/ui_tooltip_effect") as GameObject);
        ToolTip.GetComponent<UI_ToolTip_Effect>().SetEffect(effect);
        ToolTip.GetComponent<RectTransform>().SetParent(this.transform, false);
        ToolTip.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void HideItemToolTip()
    {

        
        ToolTip.SetActive(false);
    }

}
