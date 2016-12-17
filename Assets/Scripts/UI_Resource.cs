using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_Resource : MonoBehaviour, IToolTip {

    public Text Text;
    public Image Img;

    public Color NegativeColor;
    public Color PositiveColor;
    public Color ZeroColor;

    public float CountTime;

    int current_count;

    public void SetCount(int new_count)
    {
        
        Text.CountToInt(current_count, new_count, CountTime);
        current_count = new_count;

        if(new_count < 0)
        {
            Text.color = NegativeColor;
        }
        if (new_count == 0)
        {
            Text.color = ZeroColor;
        }
        if (new_count > 0)
        {
            Text.color = PositiveColor;
        }
 
    }

 
    public object GetItem()
    {
        return null;
    }
}
