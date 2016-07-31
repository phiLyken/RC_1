using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Inventory : MonoBehaviour {
    public EventHandler OnInventoryUpdated;
    public List<ItemInInventory> Items;

    public virtual void AddItem(IInventoryItem item, int count)
    {
        if (Items == null) Items = new List<ItemInInventory>();

        if (HasItem(item.GetItemType(), 1))
        {
            ModifyItem(item.GetItemType(), count);
        } else {
            Items.Add(new ItemInInventory(item, count));
        }
    }

    public bool HasItem(ItemTypes type, int Minimum)
    {
        if (Items == null) return false;
        if (Items.Count == 0) return false;


        IInventoryItem item = GetItem(type);

        return item != null && item.GetCount() >= Minimum;

        

    }

    public int ItemCount(ItemTypes type)
    {
        if (Items == null) return 0;
        if (Items.Count == 0) return 0;


        IInventoryItem item = GetItem(type);
        if (item != null) return item.GetCount();

        return 0;

    }

    public virtual void ModifyItem(ItemTypes type, int _delta)
    {
        if (Items == null) return;
        if (Items.Count == 0) return;

        IInventoryItem item = GetItem(type);
        if (item != null) item.SetCount( Mathf.Max(0,item.GetCount() + _delta));

        if (OnInventoryUpdated != null) OnInventoryUpdated();
      
    }

    public List<IInventoryItem> GetVisibleItems()
    {
        return Items.Where(it => it.GetItemType() == ItemTypes.armor || it.GetItemType() == ItemTypes.weapon).Select(ii => ii.GetItem()).ToList();
    }

    public IInventoryItem GetItem(ItemTypes type)
    {
        return Items.Where(it => it.GetItemType() == type).FirstOrDefault();
    }

    public ItemInInventory GetInventoryItem(ItemTypes type)
    {
        return Items.Where(it => it.m_item.GetItemType() == type).FirstOrDefault();
    }

}

/// <summary>
/// Wrapper for inventory item so we can count them and dont modify the games assets for those items. 
/// </summary>
[System.Serializable]
public class ItemInInventory : IInventoryItem
{

    public int max;
    public int count;
    public IInventoryItem m_item;
    public IInventoryItem GetItem()
    {
        return m_item;
    }
    public ItemInInventory(IInventoryItem _base, int startCount)
    {
        m_item = _base;
        count = startCount;
        max = startCount;
    }

    public void SetMax(int _max)
    {
        max = _max;
    }
    public void AddToInventory(UnitInventory inv)
    {
        m_item.AddToInventory(inv);
    }

    public int GetCount()
    {
        return count;
    }

    public string GetDescription()
    {
        return m_item.GetDescription();
    }

    public string GetID()
    {
        return m_item.GetID();
    }

    public Sprite GetImage()
    {
        return m_item.GetImage();
    }

    public void RemoveFromInventory(UnitInventory inv)
    {
        m_item.RemoveFromInventory(inv);
    }

    public void SetCount(int new_count)
    {
        count = Mathf.Min(max,new_count);
    }

    public ItemTypes GetItemType()
    {
        return m_item.GetItemType();
    }
}