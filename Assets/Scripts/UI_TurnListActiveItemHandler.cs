using UnityEngine;
using System.Collections;

public class UI_TurnListActiveItemHandler : MonoBehaviour {

    UI_TurnListItem m_ui;
 
    void Awake()
    {        
        m_ui = GetComponent<UI_TurnListItem>();
 
        if(TurnSystem.Instance != null)
        {
            TurnSystem.Instance.OnStartTurn += OnStartTurn;
            m_ui.UpdateActiveTurnIndicator();
        }
    }


    void OnStartTurn(ITurn started)
    {
         m_ui.UpdateActiveTurnIndicator();  
    }

    void OnDestroy()
    {
        if (TurnSystem.Instance != null)
        {
            TurnSystem.Instance.OnStartTurn -= OnStartTurn;
        }
    }
}
