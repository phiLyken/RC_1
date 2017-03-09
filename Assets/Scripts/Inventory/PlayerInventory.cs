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
            DefaultItems.ForEach(item => AddItem(item,true));
        }
    }

    public static void CheatDust(int amount)
    {
        Instance.ModifyItem(ItemTypes.dust, amount);
    }
    void Update()
    {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.Keypad0))
        {
            CheatDust(100);
        }
    }
    
    void OnGameEnd()
    {
        
    }

    void HandleEndGameProgress()
    {

    }
}


