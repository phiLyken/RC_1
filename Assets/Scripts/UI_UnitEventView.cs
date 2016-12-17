using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_UnitEventView : MonoBehaviour {

    public Text TF;
    public Image IMG;

    public void SetEvent(Sprite sprite, string text)
    {
        TF.text = text;
        IMG.sprite = sprite;
    }
    
    
}
