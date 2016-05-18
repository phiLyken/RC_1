using UnityEngine;
using System.Collections;

public class UI_TurnListActivityIndicator : MonoBehaviour {


    UI_TurnListItem m_ui;

    void Awake()
    {
        Unit.OnUnitHover += OnUnitHover;
        Unit.OnUnitHoverEnd += OnUnitUnHover;
    }

    bool isMyItem(Unit u, ITurn t)
    {
        if (u == null || t == null) return false;
        if (t as Unit != null && t as Unit == u) return true;

        return false;
    }
    void OnUnitHover(Unit u)
    {
        if(m_ui == null) { 
            m_ui = GetComponent<UI_TurnListItem>();
        }

        m_ui.SetActiveIndicator(isMyItem(u, m_ui.GetTurnable()));
    }

    void OnUnitUnHover(Unit u)
    {
        Debug.Log("ASDSADSADSADSADSAD");    
        m_ui.SetActiveIndicator(false);
    }
}
