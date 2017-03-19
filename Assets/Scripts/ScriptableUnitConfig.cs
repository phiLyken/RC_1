using UnityEngine;
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

    public UnitSpeechConfig SpeechConfig;

    public EnemyDropCategory LootCategory;

    public Sprite UnidentfiedSprite;

    public bool IdentifyOnSpawn;

    public BlipBehaviour BlipBehavior;
    // UnitInventoryConfig InventoryConfig;

    public AudioClip GetHitSound;

}

public enum BlipBehaviour
{
    on_action, always, none
}

[System.Serializable]
public class UnitInventoryConfig
{

 
    public UnitInventoryItemConfig start_heal_charges;
    public UnitInventoryItemConfig start_special_charges;
    public UnitInventoryItemConfig start_rage_charges;
}

[System.Serializable]
public class UnitInventoryItemConfig
{
    public ItemTypes type;
    public bool use_max = true;
    public int fixed_amount;

    public UnitInventoryItemConfig(ItemTypes _type)
    {
        type = _type;
    }
}