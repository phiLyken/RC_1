using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Tile_Loot : TileComponent {

    public ItemTypes Category;

    public GameObject crate;

    public override TileComponents GetComponentType()
    {
        return TileComponents.loot;
    }

    public void SetLoot(LootConfig loot)
    {

        crate = Instantiate(loot.WorldObject);

        crate.transform.position = gameObject.GetComponent<Tile>().GetPosition();
    }

    public static void AddLoot(Tile target)
    {
      //  target.gameObject.AddComponent<Tile_Loot>().SetLoot(count);
   } 
    public void RemoveLoot()
    {
        Destroy(crate);
        Destroy(this); 
    }    
    
    public void OnLoot(Unit _u)
    {
     //   PlayerInventory.Instance.AddItem( Resources.Load("Items/dust") as Item_Generic, count);
    }

    
}


