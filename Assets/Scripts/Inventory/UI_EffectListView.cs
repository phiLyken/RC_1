using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class UI_EffectListView : MonoBehaviour
{

    public ViewList<UnitEffect, UI_EffectItemView> views;

    Unit_EffectManager effects;


    List<UnitEffect> GetDisplayableEffects(List<UnitEffect> effects)
    {
        return effects.Where(eff => eff.MaxDuration > 0).ToList();
    }

    public void SetEffects(Unit_EffectManager new_effects)
    {
        views = new ViewList<UnitEffect, UI_EffectItemView>();
        views.Init(MakeNewView);
        
        if (effects != null)
        {
            effects.OnEffectAdded -= OnEffectUpdated;
            effects.OnEffectRemoved -= OnEffectUpdated;
        }

        effects = new_effects;

        if (effects != null)
        {
            effects.OnEffectAdded += OnEffectUpdated;
            effects.OnEffectRemoved += OnEffectUpdated;
        }

        OnEffectUpdated(null);
    }

    void OnEffectUpdated(UnitEffect eff)
    {
        views.UpdateList( GetDisplayableEffects(effects.ActiveEffects));
    }

    UI_EffectItemView MakeNewView(UnitEffect item)
    {
 
        UI_EffectItemView view1 = (Instantiate(Resources.Load("UI/ui_effect_list_view") as GameObject).GetComponent<UI_EffectItemView>());
        view1.SetEffect(item);
        view1.transform.SetParent(this.transform, false);
        return view1;

    }
}
