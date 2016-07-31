using UnityEngine;
using System.Collections;

public class ScriptableUnitConfig : ScriptableObject
{

    public string ID;
  
    public StatInfo[] stats;

    public UnitActionBase[] Actions;

    public int Owner;
    public int TriggerRange;

    public GameObject Mesh;

    public int UnitPower;

    public Weapon Weapon;
    public Armor Armor;

    public EnemyDropCategory LootCategory;
}

[System.Serializable]
public enum StatType
{
    complex, simple
}
