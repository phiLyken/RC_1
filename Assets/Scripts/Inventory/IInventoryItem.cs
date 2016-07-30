using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemTypes
{
    weapon,
    armor,
    buff,
    resource
}

 
public interface IInventoryItem {


     ItemTypes GetType();
     int GetCount();
     string GetID();
     Sprite GetImage();
     string GetDescription();
     void SetCount(int new_count);

     void AddToInventory(UnitInventory inv);
     void RemoveFromIntory(UnitInventory inv);

}
