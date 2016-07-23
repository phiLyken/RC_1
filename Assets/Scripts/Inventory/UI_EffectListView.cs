using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class UI_EffectListView : MonoBehaviour
{

    public Dictionary<UnitEffect , UI_EffectItemView> views;

    Unit_EffectManager effects;

    public void SetEffects(Unit_EffectManager new_effects)
    {
        if (effects != null)
        {
            effects.OnEffectAdded -= OnUpdated;
            effects.OnEffectRemoved -= OnUpdated;
        }

        effects = new_effects;

        if (effects != null)
        {
            effects.OnEffectAdded += OnUpdated;
            effects.OnEffectRemoved += OnUpdated;
        }
      
        OnUpdated(null);
    }

    void OnUpdated(UnitEffect _effect)
    {
       
        if (views == null)
        {
            views = new Dictionary<UnitEffect, UI_EffectItemView>();
        }
        
        foreach (var item in effects.ActiveEffects)
        {
            if (!views.ContainsKey(item) && item.MaxDuration > 0)
            {
                Debug.Log("Create Key ");
                MakeNewView(item);
            }
        }

        List<UnitEffect> to_remove = new List<UnitEffect>();

        foreach (var item in views.Keys)
        {

            if (!effects.ActiveEffects.Contains(item))
            {
                to_remove.Add(item);
            }
        }

        foreach (var item in to_remove)
        {
            Debug.Log("removing items not in inventory " + views[item].gameObject.name);

            Destroy(views[item].gameObject);
            views.Remove(item);

        }
    }
    static int id;
    void MakeNewView(UnitEffect item)
    {
        id++;
        UI_EffectItemView view1 = (Instantiate(Resources.Load("UI/ui_effect_list_view") as GameObject).GetComponent<UI_EffectItemView>());
        view1.SetEffect(item);
        view1.transform.SetParent(this.transform, false);
        view1.gameObject.name = id.ToString();
        views.Add(item, view1);

    }
}
