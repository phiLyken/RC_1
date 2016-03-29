using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Unit : MonoBehaviour {

    public Text UnitName;
    public UI_Bar WillBar;
    public UI_Bar IntensityBar;
    public Text MoveField;

   
    public static UI_Unit CreateUnitUI()
    {
        GameObject obj = (Instantiate(Resources.Load("unit_ui")) as GameObject);
        obj.transform.parent = GameObject.FindGameObjectWithTag("UI").transform;
        return obj.GetComponent<UI_Unit>();

    }


    public void SetUnitInfo(Unit m_unit)
    {
        UI_WorldPos worldpos = GetComponent<UI_WorldPos>();
        worldpos.SetWorldPosObject(m_unit.transform);

        UnitName.text = m_unit.name;
        WillBar.SetProgress(m_unit.Stats.GetStat(UnitStats.Stats.will).GetProgress());
        WillBar.SetBarText(m_unit.Stats.GetStat(UnitStats.Stats.will).current + "/" + m_unit.Stats.GetStat(UnitStats.Stats.will).max);

        IntensityBar.SetProgress(m_unit.Stats.GetStat(UnitStats.Stats.intensity).GetProgress());
        IntensityBar.SetBarText(m_unit.Stats.GetStat(UnitStats.Stats.intensity).current + "/" + m_unit.Stats.GetStat(UnitStats.Stats.intensity).max);
        MoveField.text = m_unit.HasAP(1).ToString();
    }

   

}
