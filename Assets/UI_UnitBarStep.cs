using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_UnitBarStep : MonoBehaviour {


    public Image Fill;
    public Image Border;

    public void SetStep(Color fill, Color border)
    {
        Fill.color = fill;
        Border.color = border;
    }
}
