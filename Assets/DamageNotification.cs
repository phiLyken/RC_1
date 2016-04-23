using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageNotification : MonoBehaviour {

    public Image BonusDmgImg;
    public Text BonusDmgText;

    public Text DamageTF;

    public static void SpawnDamageNotification(Transform obj, Damage damage)
    {
        GameObject new_obj = Instantiate(Resources.Load("damagenotif")) as GameObject;
        DamageNotification dmg_notif = new_obj.GetComponent<DamageNotification>();
        new_obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
        dmg_notif.SetInfo(obj, damage);

       

    }

    void SetInfo(Transform tr, Damage dmg)
    {
        GetComponent<UI_WorldPos>().SetWorldPosObject(tr);
        bool bonus = dmg.bonus_damage > 0;

        BonusDmgImg.gameObject.SetActive(bonus);
        BonusDmgText.gameObject.SetActive(bonus);
        if (bonus)
        {
            BonusDmgText.text = dmg.bonus_damage.ToString();
        }

        DamageTF.text = dmg.amount.ToString();

    }

    void Update()
    {
        GetComponent<UI_WorldPos>().UpdatePos();
    }
}
