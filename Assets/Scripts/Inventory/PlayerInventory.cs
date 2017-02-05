using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : Inventory {

    public List<IInventoryItem> DefaultItems;
    

    public static PlayerInventory Instance
    {
        get { return _instance == null ? _makeInstance() : _instance; }
    }

    static PlayerInventory _instance;

    static PlayerInventory _makeInstance()
    {
        GameObject obj = Instantiate(Resources.Load("player_inventory")) as GameObject;
        _instance = obj.GetComponent<PlayerInventory>();

        _instance.Init();

        DontDestroyOnLoad(obj);

        return _instance;
    }

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


