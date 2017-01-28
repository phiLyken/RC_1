using UnityEngine;
using System.Collections;

public class ObjectOnScreenEvents_Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ObjectOnScreenEvents>().OnAppear += () =>
        {
            GetComponent<Renderer>().material.color = Color.red;
        };

        GetComponent<ObjectOnScreenEvents>().OnDisappeared += () =>
        {
            GetComponent<Renderer>().material.color = Color.black;
        };
    }

}
