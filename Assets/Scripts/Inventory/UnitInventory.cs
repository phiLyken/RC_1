using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitInventory : Inventory {
    
    public Weapon EquipedWeapon;

    UnitStats m_stats;   

    public override int GetMax(ItemTypes type)
    {
        StatType maxtype;

        switch (type)
        {
            case ItemTypes.int_charge:
                maxtype = StatType.stimpack_charges_max;
                break;
            case ItemTypes.rest_pack:
                maxtype = StatType.rest_charges_max;
                break;
            case ItemTypes.stim_pack:
                maxtype = StatType.stimpack_charges_max;
                break;
            default:
                maxtype = StatType.stimpack_charges_max;
                break;
                
        }

       
        return (int) m_stats.GetStatAmount(maxtype);
    }
    public override void AddItem(IInventoryItem item, int count)
    {

        if (item.GetItemType() == ItemTypes.weapon)
        {
            EquipedWeapon = item as Weapon;
        }

        base.AddItem(item, count);
    }

    public void Init(UnitStats stats)
    {
        m_stats = stats;
    }

}
