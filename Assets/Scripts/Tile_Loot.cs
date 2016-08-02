using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Tile_Loot : TileComponent {


    public LootConfig loot;
    public GameObject crate;

    public override TileComponents GetComponentType()
    {
        return TileComponents.loot;
    }

    public void SetLoot(LootConfig _loot)
    {
        loot = _loot;
        crate = Instantiate(_loot.WorldObject);
        crate.transform.position = gameObject.GetComponent<Tile>().GetPosition();
        
        
    }

    public static void AddLoot(Tile target, LootCategory Category)
    {
        if(target.GetComponent<Tile_Loot>() == null) { 
            target.gameObject.AddComponent<Tile_Loot>().SetLoot( LootBalance.GetBalance().GetLootConfig(Category));
        }
    } 
    public void RemoveLoot()
    {
        Destroy(crate);
        Destroy(this); 
    }

    public void OnLoot(Unit _u)
    {

       
        LootContentConfig content = WeightableFactory.GetWeighted(loot.Drops);
       
        IInventoryItem item = content.Item;

        int amount = (int) content.BaseAmount.Value();

        Debug.Log("looting " + loot +" "+amount);

        if (item.GetItemType() == ItemTypes.dust)
        {
           
            amount = Constants.GetDustForProgress(amount, WorldExtender.CurrentStage);
            Debug.Log("adding " + item.GetID() + " " + amount);
            PlayerInventory.Instance.AddItem(item, amount);

        } else
        {
            Debug.Log("adding " + item.GetID() + " " + amount);
            _u.GetComponent<UnitInventory>().AddItem(item, amount);
        }

        Destroy(crate);
        Destroy(this);
        
    }

    
}


