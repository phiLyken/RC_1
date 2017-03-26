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
                    int duplicate_duration = duplicate_on_unit.GetTicksAlive();
                    new_effect.SetDurationAlive(duplicate_duration);
                    new_effect.MaxDuration = duplicate_duration + new_effect.GetMaxDuration();

;                    MDebug.Log("^effects Removing Duplicate " + duplicate_on_unit.ToString());
                    ActiveEffects.Remove(duplicate_on_unit);
                    Destroy(duplicate_on_unit);                    
                }                
            }  

            ActiveEffects.Add(new_effect);
            MDebug.Log("^effects Added Effect to " + m_Unit.GetID()+" \n" + new_effect.ToString());
            new_effect.OnEffectTick += OnEffectTick;

            //MDebug.Log(" EFFECT_ADDED " + new_effect.GetShortHandle());
            new_effect.OnEffectExpired += OnEffectExpired;

            if (OnEffectAdded != null) OnEffectAdded(new_effect);
            return true;
        } else
        {
            MDebug.Log("^effects Already has this effect");
        }

        MDebug.Log("^effects COULD NOT ADD EFFECT " + new_effect.ToString());
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
        MDebug.Log("^effects EFFECT EXPIRED " + effect.ToString());
        ActiveEffects.Remove(effect);

    }

    public void SetUnit (Unit unit)
    {
        m_Unit = unit;
    }


    
}
