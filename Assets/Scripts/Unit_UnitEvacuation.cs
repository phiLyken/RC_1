using UnityEngine;
using System.Collections;

public class Unit_UnitEvacuation : MonoBehaviour {

    Unit m_Unit;
	
    public void Init(Unit _m_unit)
    {
        m_Unit = _m_unit;
        Unit.OnEvacuated += CheckEvac;

    }

    void CheckEvac(Unit u)
    {
        if(u == m_Unit)
        {
            Unit.OnEvacuated -= CheckEvac;
            Instantiate(Resources.Load<GameObject>("Units/fx_unit_evac")).transform.position = m_Unit.transform.position;
            Destroy(m_Unit.gameObject);
        }
    }

    
}
