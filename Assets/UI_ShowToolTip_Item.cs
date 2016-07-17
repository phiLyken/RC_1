using UnityEngine;
using System.Collections;

public class UI_ShowToolTip_Item : MonoBehaviour {

    GameObject ToolTip;


       

    public void ShowItemTooTip()
    {
        SetItemToolTip(GetComponent<UI_InventoryItemView>().GetItem());
    }

    void SetItemToolTip(IInventoryItem item)
    {
        GameObject instance = null;
        Destroy(ToolTip);
        switch (item.GetType())
        {
            case ItemTypes.armor:
                instance=  Instantiate(Resources.Load("UI/ui_tooltip_armor") as GameObject);
                instance.GetComponent<UI_ToolTip_Armor>().SetArmor(item as ArmorConfig);
             
                ToolTip = instance;
                break;
                
            case ItemTypes.weapon:
                instance = Instantiate(Resources.Load("UI/ui_tooltip_weapon") as GameObject);
                instance.GetComponent<UI_ToolTip_Weapon>().SetWeapon(item as WeaponConfig);
                ToolTip = instance;
                break;
            default:
                break;
        }

        instance.GetComponent<RectTransform>().SetParent(this.transform, false);
        instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public  void HideItemToolTip()
    {
        ToolTip.SetActive(false);
    }
}
