using UnityEngine;
using System.Collections;

public class UI_ShowToolTip_Item : UI_ShowToolTip_Base {


    public GameObject ArmorToolTipPrefab;
    public GameObject WeaponToolTipPrefab;
    public GameObject GenericItemPrefab;

    protected override void SpawnToolTip(object _obj)
    {

        IInventoryItem item = (IInventoryItem)_obj;
        GameObject prefab = null;
        switch (item.GetItemType() )
        {
            case ItemTypes.armor:
                prefab = ArmorToolTipPrefab;


                break;
                
            case ItemTypes.weapon:

                prefab = WeaponToolTipPrefab;
                break;
            default:
                prefab = GenericItemPrefab;
                break;
        }
        if(prefab != null)
        ToolTipInstance = ( Instantiate(prefab) as GameObject);
      
    }


}
