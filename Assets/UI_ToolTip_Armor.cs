using UnityEngine;
using System.Collections;
using System;

public class UI_ToolTip_Armor : UI_ToolTip_Base {



    public void SetArmor(ArmorConfig conf)
    {

    }

    public override void SetItem(object obj)
    {
        SetArmor((ArmorConfig)obj);
    }
}
