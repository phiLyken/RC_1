using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Action_Bar_Skip : MonoBehaviour {
    UI_ActionBar actions;
    void Awake()
    {
        actions = GetComponentInParent<UI_ActionBar>();

    }
    public void MouseOver()
    {
        actions.Title.text = "End Turn";
        actions.TextArea.text = "Ends the turn for almost no time cost";
    }

    public void MouseOverEnd()
    {
        actions.Title.text = "";
        actions.TextArea.text = "";
    }
}
