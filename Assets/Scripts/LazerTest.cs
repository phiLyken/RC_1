using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazerTest : MonoBehaviour {

    public List<Vector3> positions;
    public Color c;
    public float t;

	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Fire1"))
        {
           SetLazer.MakeLazer(t, positions, c);
        }
	}
}
