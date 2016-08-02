using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitInventory : Inventory {
    
    public Weapon EquipedWeapon;
    public Armor EquipedArmor;


    public override int GetMax(ItemTypes type)
    {
        return 3;
    }
    public override void AddItem(IInventoryItem item, int count)
    {
        if(item.GetItemType() == ItemTypes.armor)
        {
            EquipedArmor = item as Armor;
        }

        if (item.GetItemType() == ItemTypes.weapon)
        {
            EquipedWeapon = item as Weapon;
        }

        base.AddItem(item, count);
    }


}
