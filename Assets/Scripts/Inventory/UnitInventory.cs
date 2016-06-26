using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitInventory : MonoBehaviour {

    public InventoryItem Weapon;
    public InventoryItem Armor;
    Unit Owner;

    public List<InventoryItem> Buffs;

    public void AddBuff(InventoryItem buff)
    {
        if(Buffs == null)
        {
            Buffs = new List<InventoryItem>();
        }

        Buffs.Add(buff);
    }


}
