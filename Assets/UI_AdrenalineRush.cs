using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_AdrenalineRush : MonoBehaviour {

    public Text TF;
    
    public void Init(UnitStats unit_stats)
    {
        TF.text = Constants.GetAdrenalineRushBonus(unit_stats).ToString();
        
    }

    
}
