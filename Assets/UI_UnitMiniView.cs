using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_UnitMiniView : GenericView<ScriptableUnitConfig> {

    public Image Portrait;
    public Image Border;

    public override void Remove()
    {
       
    }

    public override void Updated()
    {
        
    }

    protected override void OnSet(ScriptableUnitConfig item)
    {
        throw new NotImplementedException();
    }

    public void OnSet(ScriptableUnitConfig item, Color frame_color)
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
        view.transform.localScale = Vector3.one;
        view.OnSet(config, col);
        return view;
    }
}
