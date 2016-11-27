using UnityEngine;
using System.Collections;

[System.Serializable]
public class AbilityChargeController
{
    Unit m_unit;

    public bool useCharges;
    public ItemTypes ChargeItemType;

    public void Init(Unit u)
    {
        m_unit = u;

      
    }


    public void UseCharge()
    {
        if (useCharges)
        {

            m_unit.Inventory.ModifyItem(ChargeItemType, -1);
        }
    }
 

    void SetCharges(int charges)
    {
        IInventoryItem item_config = LootBalance.GetBalance().GetItem(ChargeItemType);

        if (item_config == null)
        {
            Debug.LogWarning("CAN NOT FIND A CONSUMABLE FOR " + ChargeItemType.ToString());
            return;
        }

        m_unit.Inventory.AddItem(item_config, charges);
    }

    public void ResetCharge()
    {

        if (useCharges)
        {
           // SetCharges(m_unit.Inventory.GetMax(ChargeItemType));
            
            
        }

    }
    public int GetMax()
    {
        return m_unit.Inventory.GetMax(ChargeItemType);
    }
    public bool HasCharges()
    {
        bool r = !useCharges || GetChargesForType() > 0;
       /* if (!r)
            Debug.Log("No Charges");*/
        return r;
    }

    public int GetChargesForType()
    {
        ItemInInventory item = m_unit.Inventory.GetItem(ChargeItemType);
        if (item == null)
        {
            Debug.LogWarning("COULDNT FIND ITEM FOR TYPE " + ChargeItemType);
            return 0;
        }
        return item.GetCount();
    }
}