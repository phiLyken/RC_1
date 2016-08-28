using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]

public class RandomMaterialSelector : MonoBehaviour {
       
    public Material[] randomMaterials;
    public GameObject[] coloredObjects;
    public List<Color> TintColors;
    // Use this for initialization
    void Start()
    {
        foreach (GameObject thing in coloredObjects)
        { //see all objects
          //Assign random material to object
            thing.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];

            if (TintColors.Count > 0)
            {
                Color c = TintColors[Random.Range(0, TintColors.Count)];

                    thing.GetComponent<Renderer>().material.color = c;
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
