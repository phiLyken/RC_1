using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SkinnedMeshRenderer))]

public class RandomMeshSelector : MonoBehaviour {
       
	public GameObject[] targetObject;
	public GameObject[] randomObject;

    // Use this for initialization
    void Start()
    {
		foreach (GameObject target in targetObject)
		{ 
			GameObject newObject = Instantiate(randomObject[Random.Range(0, randomObject.Length)]) as GameObject;
			newObject.transform.parent = target.transform;

		}
			
    }

    // Update is called once per frame
    void Update () {
	
	}
}

//targetList[0]