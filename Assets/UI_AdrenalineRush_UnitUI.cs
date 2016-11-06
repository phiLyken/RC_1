using UnityEngine;
using System.Collections;

public class UI_AdrenalineRush_UnitUI : UI_AdrenalineRushBase {

    protected override void UpdateBonus(int _bonus)
    {
        Target.SetActive(_bonus > 1);
    }

    public GameObject Target;
}
