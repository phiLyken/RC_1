using UnityEngine;
using System.Collections;

public class Unit_SoundController : MonoBehaviour {

    Unit m_unit;

    public void Init(Unit unit)
    {
        m_unit = unit;
        unit.OnDamageReceived += DamageReceived;
        Unit.OnEvacuated += CheckEvac;

    }

    void DamageReceived(UnitEffect_Damage dmg)
    {
        if(m_unit.Config.GetHitSound != null)
             SoundManager.PlaySFX(m_unit.Config.GetHitSound, m_unit.transform);
    }

   
    void CheckEvac(Unit u)
    {
        if(u == m_unit)
        {
            SoundManager.PlaySFX(Resources.Load("Sounds/sfx_evacuate") as AudioClip, m_unit.transform);
        }
    }

    void OnDestroy()
    {
        Unit.OnEvacuated -= CheckEvac;
    }
}
