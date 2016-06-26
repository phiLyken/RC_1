using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;

public class UI_InventoryView : MonoBehaviour {


    public GameObject InventoryArea;

    //TODO
    public Text Display;

    public void SetInventory(UnitInventory inv)
    {
      //  Debug.Log("Set Inventory View");
        string _inventory_as_text = "Inventory \n";


        foreach(InventoryItem i in inv.Buffs)
        {
            _inventory_as_text += i.ID + "\n";
        }

        Display.text = _inventory_as_text;
    }    
}
