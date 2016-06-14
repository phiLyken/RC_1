using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_ActionBar : MonoBehaviour {
    
    public GameObject ButtonPrefab;
    public Transform ButtonAnchor;
    public Text Title;
    public Text TextArea;

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

    void _setActions(ActionManager manager)
    {
        UpdateButtons(manager);
    }

    void OnSelectAction(UnitActionBase action)
    {
        if(action == null)
        {

            Title.text = "--";
            TextArea.text = "???";
            return;
        }
        Title.text = action.ActionID;
        TextArea.text = action.Descr;
    }

    void UnSelectAction(UnitActionBase action)
    {
        Title.text = "xxx";
        TextArea.text = "!";
    }


    /// <summary>
    /// Updates amount of ability buttons by adding them to the array, and then disabling/enabling
    /// </summary>
    /// <param name="actions"></param>

    ActionManager m_manager;

    void UpdateButtons(ActionManager manager)
    {
        Debug.Log("update buttons");
        if(m_manager != null && manager != m_manager)
        {             
            UnSelectAction(null);
      
        }

        if (manager == null) return;
        if(m_manager != null)
        {
            m_manager.OnActionSelected -= OnSelectAction;
            m_manager.OnActionUnselected -= UnSelectAction;
        }

        m_manager = manager;
        m_manager.OnActionSelected += OnSelectAction;
        m_manager.OnActionUnselected += UnSelectAction;

        int diff = manager.Actions.Length - CurrentButtons.Count;

       
        for (int i = 0; i < diff; i++)
        {
            GameObject newGO = Instantiate(ButtonPrefab);
            newGO.transform.SetParent(ButtonAnchor, false);
            UI_ActionBar_Button button = newGO.GetComponent<UI_ActionBar_Button>();
            button.OnActionHovered += OnSelectAction;           

            CurrentButtons.Add(button);
            
        }
         
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
