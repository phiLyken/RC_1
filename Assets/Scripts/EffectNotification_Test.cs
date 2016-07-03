using UnityEngine;
using System.Collections;

public class EffectNotification_Test : MonoBehaviour {

    public GameObject[] anchors;
    int index;

    public UnitEffect_Damage dmg;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            index = (index + 1) % anchors.Length;

            EffectNotification.SpawnDamageNotification(anchors[index].transform, dmg);
        }
    }
}
