using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_UnitListView : MonoBehaviour {

    public Color FrameColor;

    ViewList<ScriptableUnitConfig, UI_UnitMiniView> views;

 

    public void Init(List<ScriptableUnitConfig> configs)
    {
        views = new ViewList<ScriptableUnitConfig, UI_UnitMiniView>();
        views.Init(MakeView, delegate
        { return this.transform; }, configs, Remove, 100);
    }

    UI_UnitMiniView MakeView(ScriptableUnitConfig config, Transform target)
    {

        UI_UnitMiniView view = UI_UnitMiniView.MakeView(config, FrameColor, target);
        
        return view;
    }

    void Remove(UI_UnitMiniView view)
    {
        Destroy(view.gameObject);
    }
   
}
