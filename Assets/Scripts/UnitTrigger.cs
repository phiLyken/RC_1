using UnityEngine;
using System.Collections;

public class UnitTrigger : MonoBehaviour {


    public int OwnerTrigger;
    public UnitEventHandler OnTriggered;


    Unit Target;

    public void SetTarget(Unit unit)
    {
        Target = unit;
        OnTriggered += Target.Identify;
    }
    void OnTriggerEnter(Collider col)
    {
        Unit u = col.GetComponent<Unit>();

        if(u != null && u.OwnerID == OwnerTrigger  )
        {
           if(OnTriggered != null)
            {
                OnTriggered(u);
            }

            OnTriggered -= Target.OnIdentify;
            this.gameObject.SetActive(false);
        }
    }
}
