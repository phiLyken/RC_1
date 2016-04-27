using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_DmgPreview : MonoBehaviour {

    public Text DamageTF;
    public Text BonusTF;

    public Image BonusBG;

    public static UI_DmgPreview Instance;

    public void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        UnitAction_Attack.OnTarget += SetDamage;
        Disable();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void SetDamage(Unit instigator, Unit target, Damage dmg)
    {
        Debug.Log("assad");
        gameObject.SetActive(true);

        DamageTF.text = dmg.min + "-" + dmg.max;

        bool showBonus = dmg.bonus_damage > 0;
        BonusBG.gameObject.SetActive(showBonus);
        BonusTF.gameObject.SetActive(showBonus);
        if (showBonus)
        {
            BonusTF.text = dmg.bonus_damage.ToString();
        }

        GetComponent<UI_WorldPos>().SetWorldPosObject(target.transform); 
    }

    void Update()
    {
        GetComponent<UI_WorldPos>().UpdatePos();
    }

}
