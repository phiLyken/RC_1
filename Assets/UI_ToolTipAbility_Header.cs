using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ToolTipAbility_Header : MonoBehaviour
{

    public GameObject EndsTurnIndicator;
    public Image Icon;
    public Text Name;
    public Text Description;

    public virtual void SetAbility(UnitActionBase ability)
    {
 
        Icon.sprite = ability.GetImage();
        Name.text = ability.GetActionID();
        Description.text = ability.GetDescription();
    

        EndsTurnIndicator.SetActive(ability.GetEndsTurn());
    }

 


}
