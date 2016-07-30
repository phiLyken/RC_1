using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

   
    public List<IInventoryItem> Items;

    public void AddItem(IInventoryItem item, int count)
    {
        if (Items == null) Items = new List<IInventoryItem>();

        if (HasItem(item.GetID(), 1))
        {
            ModifyItem(item.GetID(), count);
        }

        Items.Add(item);
    }

    public static PlayerInventory Instance;

    public bool HasItem( string Id, int Minimum )
    {
        if (Items == null) return false;
        if (Items.Count == 0) return false;

        foreach (IInventoryItem item in Items)
        {
            if (item.GetID() == Id) return item.GetCount() >= Minimum;
        }

        return false;

    }

    public void ModifyItem(string Id, int _delta)
    {
        if (Items == null) return ;
        if (Items.Count == 0) return ;

        foreach (IInventoryItem item in Items)
        {
            if (item.GetID() == Id)
            {
                item.SetCount(item.GetCount() + _delta);
            }
        } 
    }

}
