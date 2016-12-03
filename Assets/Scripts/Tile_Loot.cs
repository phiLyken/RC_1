using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Tile_Loot : TileComponent {


    public LootConfig loot;
    public GameObject crate;
    IInventoryItem item_lootable;


    public override TileComponents GetComponentType()
    {
        return TileComponents.loot;
    }

    public void SetLoot(LootConfig _loot)
    {
        if(_loot == null)
        {
            Destroy(this);
            return;

        }

        loot = _loot;
        crate = Instantiate(_loot.WorldObject);

        Tile t = gameObject.GetComponent<Tile>();
        crate.transform.position = t.GetPosition();
        crate.transform.SetParent(t.transform, true);
        LootContentConfig content = WeightableFactory.GetWeighted(loot.Drops);
        

        int amount = (int) content.BaseAmount.Value();

        if (content.Item.Type  == ItemTypes.dust)
        {
            amount = Constants.GetDustForProgress(amount, WorldExtender.CurrentStage);
        }

        item_lootable = new ItemInInventory(content.Item, amount);
        

       // Debug.Log("Set loot for " + item_lootable.GetItemType() + ":" + item_lootable.GetCount());
    }


    public static void AddLoot(Tile target, LootCategory Category)
    {
        if(target.GetComponent<Tile_Loot>() == null) {
            
            target.gameObject.AddComponent<Tile_Loot>().SetLoot( LootBalance.GetBalance().GetLootConfig(Category));
        } else
        {
            Debug.LogWarning("Tile already has a loot "+target.name);
        }
    } 

     public static void AddLoot(Tile target, EnemyDropCategory Category)
    {
        LootConfig  conf = LootBalance.GetBalance().GetLootConfig(Category);
        
        if( conf != null && target.GetComponent<Tile_Loot>() == null) {
          
            target.gameObject.AddComponent<Tile_Loot>().SetLoot(conf);
        }
    } 

    Inventory GetInventory(Unit unit)
    {
        Inventory inv = null;

        if (item_lootable.GetItemType() == ItemTypes.dust)
        {
            
            inv= PlayerInventory.Instance;
        }
        else
        {
            inv = unit.GetComponent<UnitInventory>();
        }

        return inv;

    }

    public ItemTypes GetLootType()
    {
        return item_lootable.GetItemType();
    }

    public int GetLootableAmount(Unit u)
    {
        int amount = item_lootable.GetCount();
        Inventory inv = GetInventory(u);
 

 
        int lootable_amount = Mathf.Min(inv.GetMax(item_lootable.GetItemType()) - inv.ItemCount(item_lootable.GetItemType()), amount);

        Debug.Log("loot amount =" + amount + " lootable =" + lootable_amount + " max=" + inv.GetMax(item_lootable.GetItemType()));

        return lootable_amount;
    }

    public void OnLoot(Unit _u)
    {

        int lootable_amount = GetLootableAmount(_u);

        item_lootable.SetCount( item_lootable.GetCount() - lootable_amount);

        GetInventory(_u).AddItem(item_lootable, lootable_amount);

        

        if(item_lootable.GetCount() == 0)
        {
            RemoveLoot();
        }

    }

    public void RemoveLoot()
    {
        Destroy(crate);
        Destroy(this);
    }
}


