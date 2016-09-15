﻿using UnityEngine;
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
        Name.text = ability.ActionID;
        Description.text = ability.Descr;

        EndsTurnIndicator.SetActive(ability.EndTurnOnUse);
    }

 


}