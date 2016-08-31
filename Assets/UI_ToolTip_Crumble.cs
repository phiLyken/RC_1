using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ToolTip_Crumble : UI_ToolTip_Base {

    public Text UnitName;

    public override void SetItem(object obj)
    {
        UnitName.text = (obj as Unit).GetID();

    }

}
