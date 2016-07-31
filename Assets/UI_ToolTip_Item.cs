using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_ToolTip_Item : UI_ToolTip_Base
{
    public Text Title;
    public Text descr;

    public override void SetItem(object obj)
    {
        IInventoryItem item = obj as IInventoryItem;

        Title.text = item.GetItemType().ToString();
        descr.text = "BLa bla bla";
    }

   
}
