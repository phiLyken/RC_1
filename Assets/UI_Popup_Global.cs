using UnityEngine;
using System.Collections;

public class UI_Popup_Global : UI_PopupController {

    public GameObject CloseButton;
    static UI_Popup_Global instance;

    void Awake()
    {
        instance = this;
    }

    public static void ShowContent(GameObject content_prefab, bool show_close_button)
    {

        UI_SetPopupContent content = instance.GetComponentInChildren<UI_SetPopupContent>();
        content.ContentPrefab = content_prefab;

   
        instance.Open();

        instance.CloseButton.SetActive(show_close_button);
        instance.CloseButton.GetComponent<UI_SetPopupState>().Linked = show_close_button;
    }

    
}
