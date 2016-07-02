using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectNotification : MonoBehaviour {


    public Image EffectIcon;
    public Text DamageTF;

    public static void SpawnDamageNotification(Transform obj, UnitEffect effect)
    {
        GameObject new_obj = Instantiate(Resources.Load("effect_notification")) as GameObject;
        EffectNotification effect_notif = new_obj.GetComponent<EffectNotification>();

        
        new_obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
        effect_notif.SetInfo(obj, effect);       

    }

    void SetInfo(Transform tr, UnitEffect effect)
    {
        GetComponent<UI_WorldPos>().SetWorldPosObject(tr);
        DamageTF.text = effect.GetString();
        EffectIcon.sprite = effect.Icon;
    }

    void Update()
    {
        GetComponent<UI_WorldPos>().UpdatePos();
    }
}
