using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectNotification : MonoBehaviour {


    public Image EffectIcon;
    public Text DamageTF;

    public static void SpawnEffectNotification(GameObject prefab, Transform target_parent, UnitEffect effect)
    {
        SpawnEffectNotification(prefab, target_parent, effect, effect.GetString());
    }


    public static void SpawnEffectNotification(GameObject prefab, Transform target_parent, UnitEffect effect, string custom_text)
    {
        GameObject new_obj = Instantiate(prefab) as GameObject;
        EffectNotification effect_notif = new_obj.GetComponent<EffectNotification>();

        new_obj.transform.SetParent(target_parent.transform, false);

        new_obj.GetComponent<UI_EffectItemView>().SetEffect(effect);
     
    }

}
