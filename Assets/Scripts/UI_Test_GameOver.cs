using UnityEngine;
using System.Collections;

public class UI_Test_GameOver : MonoBehaviour {

    public GameObject GameOverPopupContent;

    public void Test()
    {
        UI_Popup_Global.ShowContent(GameOverPopupContent, false);
    }
}
