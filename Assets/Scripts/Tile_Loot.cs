using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Tile_Loot : TileComponent {

    public GameObject loot_object;
    public int count;

    public override TileComponents GetComponentType()
    {
        return TileComponents.loot;
    }

    public void SetLoot(int _count)
    {
        count = _count;
        loot_object = Instantiate(Resources.Load("default_loot")) as GameObject;
        loot_object.transform.position = gameObject.GetComponent<Tile>().GetPosition();
    }

    public static void AddLoot(Tile target, int count)
    {
        target.gameObject.AddComponent<Tile_Loot>().SetLoot(count);
   } 
    public void RemoveLoot()
    {
        Destroy(loot_object);
        Destroy(this); 
    }    
    
    public void OnLoot(Unit _u)
    {
        PlayerInventory.Instance.AddItem( Resources.Load("Items/dust") as Item_Generic, count);
    }




}
