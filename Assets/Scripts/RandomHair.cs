using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(SkinnedMeshRenderer))]

public class RandomHair : MonoBehaviour {
       
	//public GameObject[] targetObject;
    public GameObject[] randomObject;
    public GameObject headbone;
    // Use this for initialization
    void Start()
    {
     
     //   foreach (GameObject target in targetObject)
	//	{

            GameObject newhair = Instantiate(randomObject[Random.Range(0, randomObject.Length)]) as GameObject;

            newhair.GetComponent<Transform>().position = headbone.GetComponent<Transform>().position;
            newhair.transform.parent = headbone.transform;
 
      //  }

    }

    // Update is called once per frame
    void Update () {
	
	}
}

//targetList[0]