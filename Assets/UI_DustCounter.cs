using UnityEngine;
using System.Collections;

public class UI_DustCounter : MonoBehaviour {

    public UI_Resource Resource_UI;

    public string ToolTipText;

    void Awake()
    {
        Resource_UI.GetComponent<UI_ShowToolTip_Base>().DisplayText = ToolTipText;
    }

    void Start()
    {

        if(PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.OnInventoryUpdated += UpdatedInventory;        

            UpdateResource(PlayerInventory.Instance.ItemCount(ItemTypes.dust));
        }

        Resource_UI.Img.sprite = (Resources.Load("Items/cfg_item_currency") as Item_Generic).Image;

    }

    void UpdatedInventory(IInventoryItem item, int count)
    {
        if(item.GetItemType() == ItemTypes.dust)
        {
            UpdateResource(item.GetCount());
        }
    }
    void UpdateResource(int new_count)
    {
        Resource_UI.SetCount(new_count);
    }
}
