using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ObjectiveCondition_MoveToEnemy : ObjectiveCondition
{

    Unit target_enemy;

    public override void Init(Func<bool> canComplete)
    {

       
        base.Init(canComplete);

        
        

    }

    void Update()
    {
        if(target_enemy == null)
        {
            target_enemy = Unit.GetAllUnitsOfOwner(1, true).FirstOrDefault();

            if(target_enemy != null)
                target_enemy.OnIdentify += OnClose;
        }
    }

    void OnClose(Unit u)
    {

        target_enemy.OnIdentify -= OnClose;

        StartCoroutine(M_Math.ExecuteDelayed(3f, () => ToastNotification.SetToastMessage1("A prisoner?! He must have escaped from the mines.")));
        StartCoroutine(M_Math.ExecuteDelayed(4f, Complete));

    }

}
