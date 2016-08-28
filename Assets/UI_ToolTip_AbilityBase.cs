using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ToolTip_AbilityBase : UI_ToolTip_Base {

    public GameObject EndsTurnIndicator;
    public Image Icon;
    public Text Name;
    public Text Description;
 
    public virtual void SetAbility(UnitActionBase ability)
    {
        Icon.sprite = ability.GetImage();
        Name.text = ability.ActionID;
        Description.text = ability.Descr;

        EndsTurnIndicator.SetActive(ability.EndTurnOnUse);
    }

    public override void SetItem(object obj)
    {
        SetAbility((UnitActionBase)obj);
    }


}
