using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ToolTip_Generic : UI_ToolTip_Base {
    
    public override void SetItem(object obj)
    {
        GenericToolTipTarget gen = (GenericToolTipTarget) obj;
        GetComponentInChildren<Text>().text = gen.Text.Replace("<br>","\n");
    }
}

public class GenericToolTipTarget :  IToolTip
{
    public string Text;
    public GenericToolTipTarget(string text)
    {
        Text = text;
    }

    public object GetItem()
    {
        return this;
    }
}
