using UnityEngine;
using System.Collections;

public class UnitTrigger : MonoBehaviour {

    public GameObject Target;
    public int OwnerTrigger;

    void OnTriggerEnter(Collider col)
    {
        Unit u = col.GetComponent<Unit>();
        if(u != null && u.OwnerID == OwnerTrigger)
        {
            Debug.Log("Triggered");
            Target.SendMessage("OnTrigger", SendMessageOptions.DontRequireReceiver);
        }
    }
}
