﻿using UnityEngine;
using System.Collections;

public class ScriptableUnitConfig : ScriptableObject
{

   
    public string ID;

    public UnitBaseStats BaseStats;

    public UnitActionBase[] Actions;

    public int Owner;
    public int TriggerRange;



    public int UnitPower;

    public Weapon Weapon;
    public UnitMeshConfig MeshConfig;


    public EnemyDropCategory LootCategory;
}
