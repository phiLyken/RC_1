using UnityEngine;
using System.Collections;

public class Unit_SoundController : MonoBehaviour {

    Unit m_unit;

    public void Init(Unit unit)
    {
        m_unit = unit;
        unit.OnDamageReceived += DamageReceived;

    }

    void DamageReceived(UnitEffect_Damage dmg)
    {
        if(m_unit.Config.GetHitSound != null)
             SoundManager.PlaySFX(m_unit.Config.GetHitSound, m_unit.transform);
    }

   
}
