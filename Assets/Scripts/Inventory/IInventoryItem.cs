using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemTypes
{
    weapon,
    armor,
    buff
}

 
public interface IInventoryItem {


     ItemTypes GetType();
     string GetID();
     Sprite GetImage();
     string GetDescription();

     void AddToInventory(UnitInventory inv);
     void RemoveFromIntory(UnitInventory inv);

}

/*

    TODO THIS STUFF NEEDS TO BE AN INTERFACE SO THEY CAN BE USED BY ABILITIES AND BY THE INVENTORY
public class InventoryItem_Weapon : InventoryItem {
    public AbilityWeaponTargetMode TargetMode;
    public Effect_Damage Dmg;
    public int Range;

    public InventoryItem_Weapon(AbilityWeaponTargetMode targetmode, Effect_Damage dmg, int range, ItemTypes type, string id) : base(type, id)
    {
        Dmg = dmg;
        Range = range;
        TargetMode = targetmode;
    }

}
public class InventoryItem_Armor : InventoryItem
{

 
    public int MaxStats;
    public int MoveRange;

    public InventoryItem_Armor(int maxstats, int range, ItemTypes type, string id) : base(type, id)
    {
        MaxStats = maxstats;
        MoveRange = range;
    }

}

    */