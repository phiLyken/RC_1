using UnityEngine;
using System.Collections;

public class UnitTrigger : MonoBehaviour {


    public int OwnerTrigger;
    public UnitEventHandler OnTriggered;


    public void SetTarget(UnitAI ai)
    {
        OnTriggered += ai.OnTrigger;
    }
    void OnTriggerEnter(Collider col)
    {
        Unit u = col.GetComponent<Unit>();
        if(u != null && u.OwnerID == OwnerTrigger)
        {
           if(OnTriggered != null)
            {
                OnTriggered(u);
            }
        }
    }
}
