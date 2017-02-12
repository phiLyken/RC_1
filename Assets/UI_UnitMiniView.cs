using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_UnitMiniView : GenericView<ScriptableUnitConfig> {

    public Image Portrait;
    public Image Border;
    protected Color frame_color;

    public override void Remove()
    {
       
    }

    public override void Updated()
    {
        
    }


    protected override void OnSet(ScriptableUnitConfig item)
    {

        Portrait.sprite = item.MeshConfig.HeadConfig.Heads[0].UI_Texture;
        Border.color = frame_color;

    }
    // Use this for initialization

    public static UI_UnitMiniView MakeView(ScriptableUnitConfig config, Color col, Transform tr)
    {
        GameObject obj = Instantiate( Resources.Load("UI/ui_unitminiview")) as GameObject;
        UI_UnitMiniView view = obj.GetComponent<UI_UnitMiniView>();
        view.transform.SetParent(tr);
        view.frame_color = col;
        view.transform.localScale = Vector3.one;
        view.SetItem(config);
        return view;
    }
}
