using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class WeaponConfig : MonoBehaviour,  IInventoryItem
{

    public string ID;
    public Sprite Icon;

    public int Range;

    public WeaponBehavior RegularBehavior;
    public WeaponBehavior IntAttackBehavior;

    ItemTypes IInventoryItem.GetType()
    {
        return ItemTypes.weapon;
    }

    public string GetID()
    {
        return ID;
    }

    public Sprite GetImage()
    {
        return Icon;
    }

    public string GetDescription()
    {
        return "";
    }

    public void AddToInventory(UnitInventory inv)
    {
        
    }

    public void RemoveFromIntory(UnitInventory inv)
    {
 
    }
}
