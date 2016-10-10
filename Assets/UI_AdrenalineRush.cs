using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_AdrenalineRush : MonoBehaviour {

    public Text TF;
    public Image Icon;

    UnitStats Stats;
    public static Color ADR_Color = new Color(1, 0.6f,0);

    public void Init(UnitStats unit_stats)
    {
        Stats = unit_stats;
        Stats.OnStatUpdated += UnpdateStat;

        UpdateText();
    }

 
    void UnpdateStat(Stat s)
    {
        if (s.StatType == StatType.adrenaline)
        {
            UpdateText();
        }
    }

 
    void UpdateText()
    {
        if(gameObject != null && gameObject.activeSelf)
        { 
            int bonus = Constants.GetAdrenalineRushBonus(Stats);
            TF.text = bonus > 1 ? bonus.ToString() + "x power" : "no bonus";
            TF.color = bonus > 1 ? ADR_Color : Color.white;
            Icon.color = bonus > 1 ? ADR_Color : Color.white;
        }
    }

    void OnDestroy()
    {
        Stats.OnStatUpdated -= UnpdateStat;
    }
    
}
