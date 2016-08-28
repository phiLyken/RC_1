using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class UI_EffectListView : MonoBehaviour
{

    public GameObject Prefab;
    public ViewList<UnitEffect, UI_EffectItemView> views;     

    protected  virtual List<UnitEffect>  GetDisplayableEffects(List<UnitEffect> effects)
    {
        return effects;
    }

    public void SetEffects(List<UnitEffect> new_effects)
    {
        views = new ViewList<UnitEffect, UI_EffectItemView>();
        views.Init(MakeNewView);
        UpdateViews(new_effects);
    }

    /// <summary>
    ///  passes the effects to the view manager, after filtering them
    /// </summary>
    /// <param name="effects"></param>
    protected void UpdateViews(List<UnitEffect> effects)
    {
        views.UpdateList(GetDisplayableEffects(effects));
    }


    UI_EffectItemView MakeNewView(UnitEffect item)
    {
        UI_EffectItemView view1 = (Prefab).GetComponent<UI_EffectItemView>();
        view1.SetEffect(item);
        view1.transform.SetParent(this.transform, false);
        return view1;

    }
}
