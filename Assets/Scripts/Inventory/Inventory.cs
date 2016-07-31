using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public List<IInventoryItem> Items;

    protected virtual void AddItem(IInventoryItem item, int count)
    {
        if (Items == null) Items = new List<IInventoryItem>();

        if (HasItem(item.GetType(), 1))
        {
            ModifyItem(item.GetType(), count);
        }

        Items.Add(item);
    }



    public bool HasItem(ItemTypes type, int Minimum)
    {
        if (Items == null) return false;
        if (Items.Count == 0) return false;

        foreach (IInventoryItem item in Items)
        {
            if (item.GetType() == type) return item.GetCount() >= Minimum;
        }

        return false;

    }

    public int ItemCount(ItemTypes type)
    {
        if (Items == null) return 0;
        if (Items.Count == 0) return 0;

        foreach (IInventoryItem item in Items)
        {
            if (item.GetType() == type) return item.GetCount();

        }
        return 0;

    }

    protected virtual void ModifyItem(ItemTypes type, int _delta)
    {
        if (Items == null) return;
        if (Items.Count == 0) return;

        foreach (IInventoryItem item in Items)
        {
            if (item.GetType() == type)
            {
                item.SetCount(item.GetCount() + _delta);
            }
        }
    }
}
