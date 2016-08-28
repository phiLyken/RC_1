using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UI_EffectListView_Active : UI_EffectListView {

    Unit_EffectManager effects;

    protected override List<UnitEffect> GetDisplayableEffects(List<UnitEffect> effects)
    {
        return effects.Where(eff => eff.MaxDuration > 0).ToList();
    }

    public void SetUnitEfffects(Unit_EffectManager new_effects)
    {



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

        base.SetEffects(new_effects.ActiveEffects);
    }

    void OnEffectUpdated(UnitEffect eff)
    {
        UpdateViews(GetDisplayableEffects(effects.ActiveEffects));
    }


}
