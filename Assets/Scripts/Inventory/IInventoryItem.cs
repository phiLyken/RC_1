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
