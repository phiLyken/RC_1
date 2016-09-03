using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventNotification  {

    static GameObject spawnGameObject(GameObject prefab, Transform target_parent)
    {
        GameObject new_obj = GameObject.Instantiate(prefab) as GameObject;
     

        new_obj.transform.SetParent(target_parent.transform, false);

        return new_obj;
    }

    public static void SpawnEffectNotification(GameObject prefab, Transform target_parent, UnitEffect effect)
    {
        SpawnEffectNotification(prefab, target_parent, effect, effect.GetToolTipText());
    }


    public static void SpawnEffectNotification(GameObject prefab, Transform target_parent, UnitEffect effect, string custom_text)
    {

        GameObject new_obj = spawnGameObject(prefab, target_parent);
        new_obj.GetComponent<UI_UnitEventView>().SetEvent(effect.Icon, custom_text);

    }
    public static void SpawnInventoryNotification(GameObject prefab, Transform target_parent, IInventoryItem item, int count, string custom_text)
    {
        GameObject new_obj = spawnGameObject(prefab, target_parent);    
        
         
        new_obj.GetComponent<UI_UnitEventView>().SetEvent(item.GetImage(), "+"+count+" "+  item.GetID());

    }





}
