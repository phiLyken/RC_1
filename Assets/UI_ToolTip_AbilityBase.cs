using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ToolTip_AbilityBase : UI_ToolTip_Base {

    public RectTransform HeaderTarget;
    public virtual void SetAbility(UnitActionBase ability)
    {

        Debug.Log("adasd ");
        GameObject header = Instantiate( Resources.Load("UI/ToolTips/ui_toolip_ability_header") as GameObject);
        header.transform.SetParent(HeaderTarget, false);
        header.GetComponent<UI_ToolTipAbility_Header>().SetAbility(ability);
 
    }

    public override void SetItem(object obj)
    {
        SetAbility((UnitActionBase)obj);
    }


}
