using UnityEngine;
using System.Collections;

public class UI_ShowToolTip_Item : UI_ShowToolTip_Base {


    public GameObject ArmorToolTipPrefab;
    public GameObject WeaponToolTipPrefab;

    protected override void SpawnToolTip(Object _obj)
    {

        IInventoryItem item = (IInventoryItem)_obj;
        GameObject prefab = null;
        switch (item.GetType() )
        {
            case ItemTypes.armor:
                prefab = ArmorToolTipPrefab;


                break;
                
            case ItemTypes.weapon:

                prefab = WeaponToolTipPrefab;
                break;
            default:
                break;
        }

        ToolTipInstance = ( Instantiate(prefab) as GameObject);
      
    }


}
