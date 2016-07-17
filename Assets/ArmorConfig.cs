using UnityEngine;
using System.Collections;
using System;

public class ArmorConfig : MonoBehaviour, IInventoryItem {

    public string ID;
    public Sprite Icon;
   
    public StatInfo[] BuffedStats;

    ItemTypes IInventoryItem.GetType()
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

    public void RemoveFromIntory(UnitInventory inv)
    {
       
    }
}
