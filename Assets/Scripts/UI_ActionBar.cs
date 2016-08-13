using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_ActionBar : MonoBehaviour {
    
    public GameObject ButtonPrefab;
    public Transform ButtonAnchor;


    public List<UI_ActionBar_Button> CurrentButtons;

    static UI_ActionBar instance;

    void Awake()
    {
        instance = this;
        CurrentButtons = new List<UI_ActionBar_Button>();
        ActionManager.OnActionManagerActivated += SetActions;
       
    }

    public static void SetActions(ActionManager manager)
    {
        instance._setActions(manager);
    }

    public void SkipTurn()
    {
        if(m_manager != null &&  m_manager.GetOwnerID() == 0)
        {
            m_manager.SkipTurn();
        }
    }
    void _setActions(ActionManager manager)
    {
        UpdateButtons(manager);
    }

    void OnSelectAction(UnitActionBase action)
    {

       // SetBaseStatesInButtons();
    }

    void SetBaseStatesInButtons()
    {
        foreach (UI_ActionBar_Button button in CurrentButtons) button.SetBaseState();
    }
    void UnSelectAction(UnitActionBase action)
    {

        SetBaseStatesInButtons();

    }


    /// <summary>
    /// Updates amount of ability buttons by adding them to the array, and then disabling/enabling
    /// </summary>
    /// <param name="actions"></param>

    ActionManager m_manager;

    void UpdateButtons(ActionManager manager)
    {
        if(m_manager != null && manager != m_manager)
        {             
            UnSelectAction(null);
      
        }

       // if (manager == null) return;

       // If we already have a manager, unregister the events;
        if(m_manager != null)
        {
            m_manager.OnActionSelected -= OnSelectAction;
            m_manager.OnActionUnselected -= UnSelectAction;
            m_manager = null;
        }

        m_manager = manager;

        //Binds the callback for when an action was selected        
        m_manager.OnActionSelected += OnSelectAction;
        m_manager.OnActionUnselected += UnSelectAction;
      


        //From here it is all about adjusting the number of buttons in the action bar
        //check for diff
        int diff = manager.Actions.Length - CurrentButtons.Count;

        //spawn new buttons and hook them up to the events       
        for (int i = 0; i < diff; i++)
        {
            GameObject newGO = Instantiate(ButtonPrefab);
            newGO.transform.SetParent(ButtonAnchor, false);
            UI_ActionBar_Button button = newGO.GetComponent<UI_ActionBar_Button>();
            button.OnActionHovered += OnSelectAction;           

            CurrentButtons.Add(button);
            
        }
         

        //Go through all the buttons and set the action in the button
        for(int i = 0; i < CurrentButtons.Count; i++)
        {
            bool isActive = i < manager.Actions.Length;
            CurrentButtons[i].gameObject.SetActive(isActive);
            if (isActive)
            {
                CurrentButtons[i].SetAction(manager.Actions[i], manager);     
                
            }
            
        }
    }
}
