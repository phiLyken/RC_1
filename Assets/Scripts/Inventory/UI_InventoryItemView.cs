using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_InventoryItemView : MonoBehaviour, IToolTip {

    public Image Icon;
    public Text Count;
    public Text Description;

    protected IInventoryItem m_Item;

    public IInventoryItem GetItem()
    {
        return m_Item;
    }
    public void SetItem(IInventoryItem item)
    {
        m_Item = item;
        if (Icon != null)
            Icon.sprite = item.GetImage();
        
        if (Description != null)
            Description.text = item.GetDescription();
    }

    object IToolTip.GetItem()
    {
        return GetItem();
    }
}
