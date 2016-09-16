using UnityEngine;
using System.Collections;

public class Resource_Test : MonoBehaviour {

    public UI_Resource ResourceUI;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ResourceUI.SetCount(Random.Range(-1000, 1000));
        }
    }
}
