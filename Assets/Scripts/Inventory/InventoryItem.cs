using UnityEngine;
using System.Collections;

public enum ItemTypes
{
    weapon,
    armor,
    buff
}

[System.Serializable]   
public class InventoryItem {

    public ItemTypes Type;
    public string ID;
    public Sprite Image;
    public string Description;

    public virtual void AddedItemToInventory(UnitInventory inv)
    {
        
    }

    public InventoryItem(ItemTypes type, string id)
    {
        Type = type;
        ID = id;
        
    }
}
