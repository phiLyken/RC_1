using UnityEngine;
using System.Collections;

public class UI_SetPopupContent : MonoBehaviour {

    GameObject Content;

    public GameObject ContentPrefab;

    public UI_PopupController Popup;

   
    void Awake()
    {

        Popup.OnOpenDone += ShowContent ;
        Popup.OnClose += HideCointent;

       
    }
    void SetContent()
    {
       
        ShowContent();
    }

    void HideCointent()
    {
        if (Content != null)
        {
            Content.SetActive(false);
        }
    }

    void ShowContent()
    {
        if(Content == null)
        {
            Content = ContentPrefab.Instantiate(this.transform, true);
        }

        Content.SetActive(true);
    }
}
