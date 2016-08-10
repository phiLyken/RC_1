using UnityEngine;
using System.Collections;
using System;

public class Armor : MonoBehaviour, IInventoryItem {

    public string ID;
    public Sprite Icon;

    [HideInInspector]
    public int _count;

    public Stat[] BuffedStats;

    ItemTypes IInventoryItem.GetItemType()
    {
        return ItemTypes.armor;
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

    public void SetCount(int new_count)
    {
        _count = new_count;
    }
}
