using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class UI_InventoryView : MonoBehaviour {

    public Dictionary<IInventoryItem, UI_InventoryItemView> views;

    UnitInventory inventory;

    public void SetInventory(UnitInventory inv)
    {
        if (inventory != null)
        {
            inventory.OnInventoryUpdated -= OnUpdated;
        }

        inventory = inv;
        inventory.OnInventoryUpdated += OnUpdated;

        OnUpdated();
    }

    void OnUpdated()
    {
        if(views == null)
        {
            views = new Dictionary<IInventoryItem, UI_InventoryItemView>();
        }

       foreach(var item in inventory.GetItems())
        {
            if(!views.ContainsKey(item))
            {
              //  Debug.Log("Create Key ");
                MakeNewView(item);
            }
        }

        List<IInventoryItem> to_remove = new List<IInventoryItem>();

       foreach(var item in views.Keys)
        {
           
            if (!inventory.GetItems().Contains(item))
            {               
                to_remove.Add(item);
            }
        }

        foreach(var item in to_remove)
        {
            Debug.Log("removing items not in inventory "+views[item].gameObject.name);

            Destroy(views[item].gameObject);
            views.Remove(item);
         
        }
    }
    static int id;
    void MakeNewView(IInventoryItem item)
    {
        id++;
        UI_InventoryItemView view1 = (Instantiate(Resources.Load("UI/ui_inventory_item_view") as GameObject).GetComponent<UI_InventoryItemView>());
        view1.SetItem(item);
        view1.transform.SetParent(this.transform, false);
        view1.gameObject.name = id.ToString();
        views.Add(item, view1);

    }
}
