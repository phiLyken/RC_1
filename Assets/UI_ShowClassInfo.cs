using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ShowClassInfo : MonoBehaviour {

    public Image Portrait;
    public Text Name;
    public GameObject InfoPopup;
    public ScriptableUnitConfig testConfig;
    public UI_ShowUnitInfo_Ability ATK1;
    public UI_ShowUnitInfo_Ability ATK2;
    public UI_ShowUnitInfo_Ability ATK3;

    ScriptableUnitConfig m_config;
    void Start()
    {

        InfoPopup.SetActive(false);
    }

    public void ShowUnit(ScriptableUnitConfig conf)
    {

       
        if(m_config != null && conf == m_config)
        {
            InfoPopup.SetActive(false);
            m_config = null;
            return;
        }

        m_config = conf;
        InfoPopup.SetActive(true);
        Portrait.sprite = conf.MeshConfig.HeadConfig.Heads[0].UI_Texture;
        Name.text = conf.ID;

        ATK1.SetAttack(conf.Weapon.Behaviors[0]);

        if(conf.Weapon.Behaviors.Count > 1)
        {
            ATK2.gameObject.SetActive(true);
            ATK2.SetAttack(conf.Weapon.Behaviors[1]);
        } else
        {
            ATK2.gameObject.SetActive(false);
        }

        if (conf.Weapon.Behaviors.Count > 2)
        {
            ATK3.gameObject.SetActive(true);
            ATK3.SetAttack(conf.Weapon.Behaviors[2]);
           
        } else
        {
            ATK3.gameObject.SetActive(false);
        }
    }
}
