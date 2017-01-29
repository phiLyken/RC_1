using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ObjectiveCondition_Loot : ObjectiveCondition {

    public ItemTypes type;
    public int LootAmount;

    int count;

    Unit m_Unit;
    

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
    }

    void Update()
    {
        if(m_Unit == null)
        {
            m_Unit = Unit.GetAllUnitsOfOwner(0, true).FirstOrDefault();
            if(m_Unit != null)
            {
                m_Unit.Inventory.OnInventoryUpdated += OnLoot;
            }
        }
    }

    void OnLoot(IInventoryItem item, int count)
    {
        if(item.GetItemType() == type)
        {
            count += count;
            if(count >= LootAmount)
            {
                m_Unit.Inventory.OnInventoryUpdated -= OnLoot;
                Complete();
               
            }
        }
    }
}
