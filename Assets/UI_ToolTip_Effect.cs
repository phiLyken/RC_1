using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ToolTip_Effect : MonoBehaviour {

    UnitEffect m_effect;
    public Text Description;

    public void SetEffect(UnitEffect effect)
    {
        Description.text = "Some information about " + effect.GetType().ToString();
    }
}
