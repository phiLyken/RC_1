using UnityEngine;
using System.Collections;
using System.Collections.Generic;


 
public interface IInventoryItem {


     ItemTypes GetItemType();
     int GetCount();
     string GetID();
     Sprite GetImage();
     string GetDescription();
     void SetCount(int new_count);

     void AddToInventory(UnitInventory inv);
     void RemoveFromInventory(UnitInventory inv);

}
