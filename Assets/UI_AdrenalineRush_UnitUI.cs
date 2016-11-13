using UnityEngine;
using System.Collections;

public class UI_AdrenalineRush_UnitUI : UI_AdrenalineRushBase {
    public GameObject Target;


    void Awake()
    {
        Target.SetActive(false);
    }
    protected override void RushGain()
    {
        Target.SetActive(true);
    }

    protected override void RushLoss()
    {
        Target.SetActive(false);
    }
   

   
}
