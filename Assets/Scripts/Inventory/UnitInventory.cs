using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitInventory : MonoBehaviour {

    public EventHandler OnInventoryUpdated;

    public WeaponConfig EquipedWeapon;
    public ArmorConfig EquipedArmor;

    List<IInventoryItem> Items;

    Unit Owner;

    public void AddItem(IInventoryItem new_item)
    {
        if (Items == null) Items = new List<IInventoryItem>();

        Debug.Log("Adding Item To Inventory " + new_item.GetID());
        Items.Add(new_item);

        if (OnInventoryUpdated != null) OnInventoryUpdated();
    }

    public IInventoryItem GetItem(ItemTypes type)
    {
        return Items.Where(i => i.GetType() == type).First();
    }

    public List<IInventoryItem> GetItems()
    {
        return Items;
    }

}
