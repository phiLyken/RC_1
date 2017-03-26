using UnityEngine;
using System.Collections;

public class UI_SetPopupContent : MonoBehaviour {

    GameObject Content;

    public GameObject ContentPrefab;

    public UI_PopupController Popup;

   
    void Awake()    
    {

        Popup.OnOpenDone += SetContent ;
        Popup.OnClose += HideContent;

       
    }

    void HideContent()
    {
        if (Content != null)
        {
            Content.SetActive(false);
        }
    }

 
    void SetContent()
    {
        MDebug.Log("POPUP setting content");
        if(Content != null)
        {
            Destroy(Content);
        }

        Content = ContentPrefab.Instantiate(this.transform, true);

        UI_Popup_BindToClose bind = GetComponentInChildren<UI_Popup_BindToClose>();
        if (bind != null)
        {
            bind.SetClose(Popup.Close);
        }
        Content.SetActive(true);
    }
}
