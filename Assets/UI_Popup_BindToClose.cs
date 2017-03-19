using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_Popup_BindToClose : MonoBehaviour {
    
    Action close;

    public void SetClose(Action _close)
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        close = _close;

    }

    void OnClick()
    {
        close.AttemptCall();
    }

    void OnDestroy()
    {
        close = null;
    }
	
}
