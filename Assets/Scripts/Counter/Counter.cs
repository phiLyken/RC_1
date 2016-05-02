using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Counter : MonoBehaviour {

    public GameObject[] Enabled_States;
    public GameObject[] Disabled_States;

    void Start()
    {
        if(Disabled_States != null && Disabled_States.Length > 0 && Disabled_States.Length != Enabled_States.Length)
        {
            Debug.LogWarning("Not match disabled/enabled states");
        }
    }
    public void SetNumber(int newcount)
    {
        if (newcount >= 0)
        {
            for(int i = 0; i < Enabled_States.Length; i++)
            {
                bool enabled = i < newcount;
                Enabled_States[i].SetActive(enabled);
                if (Disabled_States != null && Disabled_States.Length > 0)
                {
                    Disabled_States[i].SetActive(!enabled);
                }
            }
        }
    }
}
