using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit_EffectManager : MonoBehaviour {

    Unit m_Unit;

    public EffectEventHandler OnEffectAdded;
    public EffectEventHandler OnEffectTick;
    public EffectEventHandler OnEffectRemoved;

    public List<UnitEffect> ActiveEffects = new List<UnitEffect>();

   
    public bool AddEffect(UnitEffect new_effect)
    {
        if(ActiveEffects == null)
        {
            ActiveEffects = new List<UnitEffect>();
        }

        if (!ActiveEffects.Contains(new_effect))
        {
            if (new_effect.ReplaceEffect)
            {
                UnitEffect duplicate_on_unit = GetEffect(new_effect.Unique_ID);
                if(duplicate_on_unit != null)
                {
                    ActiveEffects.Remove(duplicate_on_unit);
                    Destroy(duplicate_on_unit);
                    
                }
                
            }
            ActiveEffects.Add(new_effect);

            new_effect.OnEffectTick += OnEffectTick;

            //Debug.Log(" EFFECT_ADDED " + new_effect.GetShortHandle());
            new_effect.OnEffectExpired += OnEffectExpired;

            Unit.OnUnitKilled += u =>
            {
                if (u == m_Unit)
                {
                    new_effect.Remove();
         
                }
            };

            if (OnEffectAdded != null) OnEffectAdded(new_effect);
            return true;
        }
        return false;
    }


    UnitEffect GetEffect(string id)
    {
        foreach (UnitEffect eff in ActiveEffects)
        {
            if (eff.Unique_ID == id) return eff;
        }
        return null;
    }
    public bool ApplyEffect(UnitEffect effect)
    {
        if( !m_Unit.IsDead()  && AddEffect(effect))
        {
            return true;
        }

        return false;
    }

    void OnEffectExpired(UnitEffect effect) {

        if (OnEffectRemoved != null) OnEffectRemoved(effect);

        effect.OnEffectExpired -= OnEffectRemoved;
        effect.OnEffectTick -= OnEffectTick;
        effect.OnEffectExpired -= OnEffectExpired;
        Debug.Log(" EFFECT_REMOVED " + effect.GetToolTipText());
        ActiveEffects.Remove(effect);

    }

    public void SetUnit (Unit unit)
    {
        m_Unit = unit;
    }


    
}
