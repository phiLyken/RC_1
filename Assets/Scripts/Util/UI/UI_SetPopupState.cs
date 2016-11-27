using UnityEngine;
using System.Collections;

public class UI_SetPopupState : MonoBehaviour {

    public UI_PopupController popup;

 
    public bool ShowOnOpend;
    public bool ShowOnOpen;
    public bool HideOnClose;
    public bool HideOnClosed;

    public bool Linked;

    void Awake()
    {
        if (HideOnClose)
            popup.OnClose += () => SetState(false);

        if (ShowOnOpend)
            popup.OnOpenDone += () => SetState(true);

        if (ShowOnOpen)
            popup.OnOpen += () => SetState(true);
        if (HideOnClosed)
            popup.OnCloseDone += () => SetState(false);
        ;

        gameObject.SetActive(popup.Active);
    }

    void SetState(bool b)
    {
        if (Linked)
        {
            gameObject.SetActive(b);
        }
    }
}
