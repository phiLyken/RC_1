using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class UI_Anim_BlinkOnEnable : MonoBehaviour {

    List<Graphic> renderers;
   
    public List<BlinkNode> Blinks;
    public bool StartEnabled;

    public bool StartOnAwake;
    public float Delay;
    public bool Loop;

    void Awake()
    {
        renderers = GetComponentsInChildren<Graphic>().ToList();
    }
    void OnEnable()
    {
        Trigger();
    }

    public void Trigger()
    {
        StopAllCoroutines();
        StartCoroutine(Sequence());
    }
    IEnumerator Sequence()
    {
        bool current = StartEnabled;
        yield return new WaitForSeconds(Delay);

        for (int i = 0; i < Blinks.Count; i++)
        {

            setAll(current);
            yield return new WaitForSeconds(Blinks[i].time);
            current = !current;

            if (Loop && i == Blinks.Count-1)
                i = 0;
        }
    }

    void setAll(bool b)
    {
        renderers.ForEach(r => r.enabled = b);
    }
}

[System.Serializable]
public class BlinkNode
{
    public float time;
}