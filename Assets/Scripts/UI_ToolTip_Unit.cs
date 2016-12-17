using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_ToolTip_Unit : UI_ToolTip_Base
{

    public Text UnitName;    
   
    public override void SetItem(object obj)
    {
        UnitName.text = (obj as Unit).GetID();
       
    }
}
