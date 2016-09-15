using UnityEngine;
using System.Collections;

public class UI_TurnListMouseOverHandler : MonoBehaviour {
    
    UI_TurnListItem m_ui;

    void Awake()
    {
        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitHoverEnd += OnUnitUnHover;
        m_ui = GetComponent<UI_TurnListItem>();
        m_ui.MouseOverIndicator.SetActive(false);
    }

    bool isMyItem(Unit u, ITurn t)
    {
        if (u == null || t == null) return false;
        if (t as Unit != null && t as Unit == u) return true;

        return false;
    }

    void OnUnitHover(Unit u)
    {
 
        m_ui.MouseOverIndicator.SetActive(isMyItem(u, m_ui.GetTurnable()));
    }

    void OnUnitUnHover(Unit u)
    {
        m_ui.MouseOverIndicator.SetActive(false);
    }

    public void OnUIHover()
    {
        if(m_ui.GetTurnable() is Unit)
        {
            (m_ui.GetTurnable() as Unit).OnHover();
        }
    }

    public void OnUIUnhover()
    {
        if (m_ui.GetTurnable() is Unit)
        {
            (m_ui.GetTurnable() as Unit).OnHoverEnd();
        }
    }
}
