using UnityEngine;
using System.Collections;
using System;

public class ObjectiveCondition_SeeEnemy : ObjectiveCondition {

    public GameObject Notifier;
   
    ObjectOnScreenEvents events;

    public override void Init(Func<bool> canComplete)
    {
        base.Init(canComplete);
        GlobalUpdateDispatcher.OnUpdate += _update;
    }

    
    void OnDestroy()
    {
        if(events != null)
            Destroy(events.gameObject);
    }

    void _update(float f)
    {
        if(events == null && Unit.GetAllUnitsOfOwner(1, true).Count > 0)
        {            
            Transform unit = Unit.GetAllUnitsOfOwner(1, true)[0].transform;

            Notifier.transform.SetParent(unit, false);

            events = Notifier.GetComponent<ObjectOnScreenEvents>();
            events.OnAppear += Found;         
            
        }
    }


    void Found()
    {
        GlobalUpdateDispatcher.OnUpdate -= _update;
        events.OnAppear -= Found;
        new TurnEventQueue.CameraFocusEvent(events.transform.position, PostPan);
       
    }

    void PostPan()
    {
        StartCoroutine(PostSequence());
  
    }
    
    IEnumerator PostSequence()
    {
        Unit.GetAllUnitsOfOwner(0, true)[0].Identify(null);
        ToastNotification.SetToastMessage1("There he is!");
        yield return new WaitForSeconds(2f);
        ToastNotification.SetToastMessage1("Commander, HQ has assigned you "+Unit.GetAllUnitsOfOwner(0, true)[0].name+" to support you solving the situation");

        //Complete will end the turn of the hacked in Tutorial Turn and then scroll to the next unit, which should be the previously identified
        this.ExecuteDelayed(3f, Complete);
        yield return null;
    }
}
