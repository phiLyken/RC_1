using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour {

    public RectTransform Bar;
    public Text BarText;

    Vector2 BarDimension;

    public void SetBarText(string text)
    {
        BarText.text = text;
    }
    public void SetProgress(float t)
    {
        if(BarDimension == Vector2.zero)
        {
            BarDimension = transform.parent.GetComponent<RectTransform>().sizeDelta;
        }
        t = Mathf.Clamp(t, 0, 1);

        Bar.sizeDelta = new Vector2(t * BarDimension.x, Bar.sizeDelta.y);
    }    
    public void SetColor(Color c)
    {
        Bar.GetComponent<Image>().color = c;
    }
    
}
