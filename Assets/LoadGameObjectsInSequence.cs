using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadGameObjectsInSequence : MonoBehaviour {
    public bool LoadOnAwake;
    public GameObject EnableOnLoad;

    public List<GameObject> ToLoad;
    public CanvasGroup Group;

	// Use this for initialization
	void Awake () {

        foreach(GameObject go in ToLoad)
        {
            go.SetActive(false);
        }

        Group.alpha = 0;
	    if(LoadOnAwake)
        {
            EnableOnLoad.SetActive(true);
            StartCoroutine(Load());
        }
	}


    IEnumerator Load()
    {
        foreach(GameObject go in ToLoad)
        {
            go.SetActive(true);
            yield return null;

        }

        EnableOnLoad.SetActive(false);
        Group.alpha = 1;
        yield break;
    }
}
