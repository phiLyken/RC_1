using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(SkinnedMeshRenderer))]

public class RandomMeshSelector : MonoBehaviour {
       
	public GameObject[] targetObject;
    public GameObject[] randomObject;
    //public Mesh[] randomObject;
    public GameObject headbone;
    // Use this for initialization
    void Start()
    {
     
        foreach (GameObject target in targetObject)
		{

      //      headbone = target.transform.Find("humanoid/humanoid Pelvis/humanoid Spine/humanoid Spine1/humanoid Neck/humanoid Head").gameObject;




            //GameObject newObject = Instantiate(randomObject[Random.Range(0, randomObject.Length)]) as GameObject;
            GameObject blubb = Instantiate(randomObject[Random.Range(0, randomObject.Length)]) as GameObject;

            blubb.GetComponent<Transform>().position = headbone.GetComponent<Transform>().position;
            blubb.transform.parent = headbone.transform;
            // target.GetComponent<SkinnedMeshRenderer>().sharedMesh = randomObject[Random.Range(0, randomObject.Length)];
            //   newObject.transform.parent = target.transform;

        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}

//targetList[0]