using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Unit : MonoBehaviour {
       
    public Text UnitName;
    public Text MoveField;
    public  UnitBar StatBar;

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

        PlayerUnitStats p_stats = u.Stats as PlayerUnitStats;
        if(p_stats != null)
        {
            StatBar.SetBarValues( p_stats.Will, p_stats.Int, p_stats.Max);
            
        } else
        {
            StatBar.SetBarValues(
                (int) u.Stats.GetStat(UnitStats.StatType.HP).Amount, 0,
                (int) u.Stats.GetStat(UnitStats.StatType.max).Amount
                );
        }


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


}
