using UnityEngine;
using System.Collections;

public class DamageNotification_Test : MonoBehaviour {

    public GameObject[] anchors;
    int index;

    public Damage dmg;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            index = (index + 1) % anchors.Length;

            DamageNotification.Spawn(anchors[index].transform, dmg);
        }
    }
}
