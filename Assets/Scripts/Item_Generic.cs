using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Item_Generic : ScriptableObject, IInventoryItem
{

    public int Count;
    public string Description;
    public string ID;
    public Sprite Image;
    public ItemTypes Type;


    public int GetCount()
    {
        return Count;
    }

    public string GetDescription()
    {
        return Description;
    }

    public string GetID()
    {
        return ID;
    }

    public Sprite GetImage()
    {
        return Image;
    }

    public void RemoveFromInventory(UnitInventory inv)
    {
      
    }
    public void AddToInventory(UnitInventory inv)
    {

    }

    public void SetCount(int new_count)
    {
        Count = new_count;
    }

    ItemTypes IInventoryItem.GetType()
    {
        return Type;
    }
}
