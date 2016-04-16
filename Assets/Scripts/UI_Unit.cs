using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Unit : MonoBehaviour {

    public Text UnitName;
    public UI_Bar WillBar;
    public UI_Bar IntensityBar;
    public Text MoveField;

    Unit m_unit;
   
    public static UI_Unit CreateUnitUI()
    {
        GameObject obj = (Instantiate(Resources.Load("unit_ui")) as GameObject);
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
        return obj.GetComponent<UI_Unit>();
    }

    void Update()
    {
        if (m_unit != null) UpdatePosition();
    }
    
    public void UpdatePosition()
    {
        UI_WorldPos worldpos = GetComponent<UI_WorldPos>();
        worldpos.SetWorldPosObject(m_unit.transform);
    }

    public void SetUnitInfo(Unit u)
    {
       
        m_unit = u;
       // UnitName.text = m_unit.name;
        WillBar.SetProgress(m_unit.Stats.GetStat(UnitStats.Stats.will).GetProgress());
        WillBar.SetBarText(m_unit.Stats.GetStat(UnitStats.Stats.will).current + "/" + m_unit.Stats.GetStat(UnitStats.Stats.will).current_max);

        IntensityBar.SetProgress(m_unit.Stats.GetStat(UnitStats.Stats.intensity).GetProgress());
        IntensityBar.SetBarText(m_unit.Stats.GetStat(UnitStats.Stats.intensity).current + "/" + m_unit.Stats.GetStat(UnitStats.Stats.intensity).current_max);
        MoveField.text = "";
        for(int i = 0; i < u.Actions.GetAPLeft(); i++)
        {
            MoveField.text += "o";
        }

        if(u.OwnerID == 0)
        {
            WillBar.Bar.GetComponent<Image>().color = new Color(0f, 0.6f,0.8f , 1);
        } else
        {
            WillBar.Bar.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
        }
       
    }

   

}
