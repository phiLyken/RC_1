using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Unit : MonoBehaviour {

    public delegate void UpdatedValue(int val);
    public Text UnitName;

    public Counter IntensityCurrentCounter;
   // public Counter IntensityMaxCounter;


    public Counter WillCurrentCounter;
    public Counter WillMaxCounter;

    public event UpdatedValue OnWillCurrentUpdate;

    public Text MoveField;

    Unit m_unit;
   
    public static void CreateUnitUI(Unit u)
    {
        GameObject obj = (Instantiate(Resources.Load("unit_ui")) as GameObject);
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
        obj.GetComponent<UI_Unit>().SetUnitInfo(u);
    }
    public void Toggle(bool active)
    {
        gameObject.SetActive(active);
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
        u.Stats.OnStatUpdated += OnUpdateStat;
        u.OnTurnStart += UpdateUI;

        u.OnTurnEnded += TurnEnd;

        Unit.OnUnitHover += CheckHovered;
        Unit.OnUnitHoverEnd += CheckHoverEnd;
        Unit.OnUnitKilled += CheckKilled;

        UpdateUI(u);
    }
    void TurnEnd(Unit u)
    {
        Toggle(false);
    }
    void OnUpdateStat()
    {
        UpdateUI(m_unit);
    }
    void CheckKilled(Unit u)
    {
        Debug.Log("asdsad");
        if(u == m_unit) {
            Unit.OnUnitKilled -= CheckKilled;
            m_unit.Stats.OnStatUpdated -= OnUpdateStat;
            Unit.OnUnitHover -= CheckHovered;
            Unit.OnUnitHoverEnd -= CheckHoverEnd;
            u.OnTurnEnded -= TurnEnd;
            Destroy(this.gameObject);
        }
        
    }

    void CheckHoverEnd(Unit _hovered)
    {
        if (_hovered == m_unit && !TurnSystem.HasTurn(m_unit))
        {
            Toggle(false);
        }

    }
    void CheckHovered(Unit _hovered)
    {
        if (_hovered == m_unit && m_unit.IsActive)
        {
            UpdateUI(m_unit);
            Toggle(true);
        }
    }
    void UpdateUI(Unit u)
    {


        UpdateWill((int)m_unit.Stats.GetStat(UnitStats.Stats.will).current);
        UpdateWillMax((int)m_unit.Stats.GetStat(UnitStats.Stats.will).current_max);
        UpdateIntensity((int)m_unit.Stats.GetStat(UnitStats.Stats.intensity).current);

        MoveField.text = "";
        for (int i = 0; i < m_unit.Actions.GetAPLeft(); i++)
        {
            MoveField.text += "o";
        }

        if (TurnSystem.HasTurn(m_unit))
        {
            Toggle(true);
        }
        else
        {
            Toggle(false);
            return;
        }
    }
    void UpdateIntensity(int value  )
    {
        IntensityCurrentCounter.SetNumber(value);
    }
   

    void UpdateWill(int value)
    {
        WillCurrentCounter.SetNumber(value);
    }


    void UpdateWillMax(int value)
    {
        WillMaxCounter.SetNumber(value);
    }

}
