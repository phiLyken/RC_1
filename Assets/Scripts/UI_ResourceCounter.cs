using UnityEngine;
using System.Collections;

public class UI_ResourceCounter : MonoBehaviour {

    public UI_Resource Resource_UI;
    public ItemTypes ItemType;
    public bool UpdateOnlyOnce;

    public string ToolTipText;

    void Awake()
    {
        Resource_UI.GetComponent<UI_ShowToolTip_Base>().DisplayText = ToolTipText;
    }

    void Start()
    {

        if(PlayerInventory.Instance != null)
        {
            if(!UpdateOnlyOnce)
                PlayerInventory.Instance.OnInventoryUpdated += UpdatedInventory;        

            UpdateResource(PlayerInventory.Instance.ItemCount(ItemType));
        }

        Resource_UI.Img.sprite = PlayerInventory.Instance.GetItem(ItemType).GetImage();

    }

    void OnDestroy()
    {
        if(!UpdateOnlyOnce &&  PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.OnInventoryUpdated -= UpdatedInventory;
        }
    }
    void UpdatedInventory(IInventoryItem item, int count)
    {
        if(item.GetItemType() == ItemType)
        {
            UpdateResource(item.GetCount());
        }
    }
    void UpdateResource(int new_count)
    {
        Resource_UI.SetCount(new_count);
    }
}
