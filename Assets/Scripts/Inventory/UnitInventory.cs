using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitInventory : MonoBehaviour {

    public EventHandler OnInventoryUpdated;

    //TODO FIX! >_<
    public WeaponConfig Weapon;
    public ArmorConfig Armor;
    Unit Owner;


    public List<StatInfo> GetAllBuffs()
    {
        List<StatInfo> all = new List<StatInfo>();

        if (Armor != null) all.AddRange(Armor.BuffedStats);

        return all;
    }
    public void SetWeapon(WeaponConfig wp)
    {
        Weapon = wp;
        if (OnInventoryUpdated != null) OnInventoryUpdated() ;
       
    }

    public void SetArmor(ArmorConfig arm)
    {
        Armor = arm;
        
        if (OnInventoryUpdated != null) OnInventoryUpdated();

    }
    public List<InventoryItem> Buffs;

   
    public void AddBuff(InventoryItem buff)
    {
        if(Buffs == null)
        {
            Buffs = new List<InventoryItem>();
        }

        Buffs.Add(buff);

        if (OnInventoryUpdated != null) OnInventoryUpdated();
    }

   
}
