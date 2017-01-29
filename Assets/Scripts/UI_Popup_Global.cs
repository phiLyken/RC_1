using UnityEngine;
using System.Collections;

public class UI_Popup_Global : UI_PopupController {

    public GameObject CloseButton;
    public  static UI_Popup_Global Instance;

    void Awake()
    {
        Instance = this;
    }

    public static void ShowContent(GameObject content_prefab, bool show_close_button)
    {

        UI_SetPopupContent content = Instance.GetComponentInChildren<UI_SetPopupContent>();
        content.ContentPrefab = content_prefab;

   
        Instance.Open();

        Instance.CloseButton.SetActive(show_close_button);
        Instance.CloseButton.GetComponent<UI_SetPopupState>().Linked = show_close_button;
    }

    
}
