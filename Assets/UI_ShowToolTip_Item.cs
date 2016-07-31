using UnityEngine;
using System.Collections;

public class UI_ShowToolTip_Item : UI_ShowToolTip_Base {


    public GameObject ArmorToolTipPrefab;
    public GameObject WeaponToolTipPrefab;
    public GameObject Default;

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
                //   prefab = Default;
                break;
        }
        if(prefab != null)
        ToolTipInstance = ( Instantiate(prefab) as GameObject);
      
    }


}
