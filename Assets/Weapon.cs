using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Weapon : MonoBehaviour,  IInventoryItem
{
    public string ID;
    public Sprite Icon;

    [HideInInspector]
    public int _count;
    public int Range;

    public WeaponBehavior RegularBehavior;
    public WeaponBehavior IntAttackBehavior;

    public StatInfo[] BuffedStats;


    ItemTypes IInventoryItem.GetItemType()
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

    public void RemoveFromInventory(UnitInventory inv)
    {
 
    }

    public int GetCount()
    {
        return _count;
    }


    void IInventoryItem.SetCount(int new_count)
    {
        _count = new_count;
    }
}
