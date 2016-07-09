using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit_EffectManager : MonoBehaviour {

    Unit m_Unit;

    public EffectEventHandler OnEffectAdded;
    public EffectEventHandler OnEffectTick;
    public EffectEventHandler OnEffectRemoved;

    public List<UnitEffect> ActiveEffects;

   
    public bool AddEffect(UnitEffect new_effect)
    {
        if(ActiveEffects == null)
        {
            ActiveEffects = new List<UnitEffect>();
        }

        if (!ActiveEffects.Contains(new_effect))
        {            
            ActiveEffects.Add(new_effect);

            new_effect.OnEffectTick += OnEffectTick;

            Debug.Log(" EFFECT_ADDED " + new_effect.GetString());
            new_effect.OnEffectExpired += OnEffectExpired;
           
            if (OnEffectAdded != null) OnEffectAdded(new_effect);
            return true;
        }
        return false;
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
        Debug.Log(" EFFECT_REMOVED " + effect.GetString());
        ActiveEffects.Remove(effect);

    }

    public void SetUnit (Unit unit)
    {
        m_Unit = unit;
    }


    
}
