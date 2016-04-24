using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Rage : UnitActionBase
{

    public float[] WillLoseChances;
    public float[] IntensityGainChance;
    void Awake()
    {
        orderID = 11;
    }

    public override void SelectAction()
    {
        base.SelectAction();
        StartCoroutine(WaitForConfirmation());
    }
    

    public override void UnSelectAction()
    {
        base.UnSelectAction();
        StopAllCoroutines(); 
    }

    IEnumerator WaitForConfirmation()
    {
        while (!Input.GetButtonUp("Jump")) {
            yield return null;
        }

        AttemptExection();

    }
    protected override void ActionExecuted()
    {
        int current_will = (int) Owner.Stats.GetStat(UnitStats.Stats.will).current;
        if (GetLooseWillOnRage(current_will))
        {
            Owner.ModifyInt(-1);
        }

        Owner.Stats.GetStat(UnitStats.Stats.intensity).ModifyStat(IntensityGained());

        base.ActionExecuted();  
    }
    public int IntensityGained()
    {
        int gained = 0;
        foreach (float f in IntensityGainChance)
        {
            gained += (f > Random.value ? 1 : 0);
           // Debug.Log(gained);
        }
        return gained;
    }
    bool GetLooseWillOnRage(int current_will)
    {
        return WillLoseChances[Mathf.Min(current_will-1,WillLoseChances.Length-1)] > Random.value;
    }
}
