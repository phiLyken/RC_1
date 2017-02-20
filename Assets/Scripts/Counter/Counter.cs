using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Counter : MonoBehaviour {

    public bool Blink;
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
            for (int i = 0; i < Enabled_States.Length; i++)
            {
                bool enabled = i < newcount;

                if (Enabled_States[i].activeSelf != enabled)
                {
                    if (Blink)
                    {
                        StartCoroutine(Enabled_States[i].Blink(0.25f, 0.25f, 10, 2, !enabled, enabled));
                    } else
                    {
                        Enabled_States[i].SetActive(enabled);
                    }
                }

                if (Disabled_States != null && Disabled_States.Length > 0)
                {
                    Disabled_States[i].SetActive(!enabled);
                    if(Blink)
                    StartCoroutine(Disabled_States[i].Blink(0.25f, 0.25f, 10, 2, true, true));
                }
            }
        }
    }
}
