using UnityEngine;
using System.Collections;

public class UI_SetPopupState : MonoBehaviour {

    public UI_PopupController popup;

 
    public bool ShowOnOpend;
    public bool ShowOnOpen;
    public bool HideOnClose;
    public bool HideOnClosed;

    void Awake()
    {
        if(HideOnClose)
            popup.OnClose += () => gameObject.SetActive(false);

        if (ShowOnOpend)
            popup.OnOpenDone += () => gameObject.SetActive(true);

        if (ShowOnOpen)
            popup.OnOpen += () => gameObject.SetActive(true);
        if (HideOnClosed)
            popup.OnCloseDone += () => gameObject.SetActive(false);

        gameObject.SetActive(popup.Active);
    }

}
