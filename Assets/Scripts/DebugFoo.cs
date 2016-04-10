﻿using UnityEngine;
using System.Collections;

public class DebugFoo : MonoBehaviour {

    public static bool DebugEnabled = true;

    Unit hoveredUnit;

	// Use this for initialization
	void Start () {
        Unit.OnUnitHover += SetHovered;
	}
	
    void SetHovered(Unit u)
    {
    //s    Debug.Log("hovered");
        hoveredUnit = u;
    }

    void Update()
    {
        if (hoveredUnit != null && Input.GetKeyDown(KeyCode.Alpha0))
        {

            hoveredUnit.ReceiveDamage(new Damage());
        }
    }
}
