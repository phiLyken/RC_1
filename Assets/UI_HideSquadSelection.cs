using UnityEngine;
using System.Collections;

public class UI_HideSquadSelection : MonoBehaviour {

    public void Hide()
    {
        UI_Menu menu = GameObject.FindObjectOfType<UI_Menu>();
        if(menu != null)
        {
            menu.ShowSquadSelection(false);
        }
    }
}
