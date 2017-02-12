using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : Inventory, IInit {

    public List<Item_Generic> DefaultItems;
    

    public static PlayerInventory Instance
    {
        get { return _instance == null ? M_Extensions.MakeMonoSingleton<PlayerInventory>(out _instance) : _instance; }
    }

    static PlayerInventory _instance;

   

    public override int GetMax(ItemTypes type)
    {
        return 99999999;
    }

    public void Init()
    {
        if (DefaultItems != null)
        {
            DefaultItems.ForEach(item => _instance.AddItem(item,true));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Instance.ModifyItem(ItemTypes.dust, 100);
        }
    }
}


