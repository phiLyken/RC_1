using UnityEngine;
using System.Collections;

public class InitCameraSpawner : MonoBehaviour {

    public Camera Prefab;
    public Transform Bounds;

    public void Awake()
    {
        GameObject obj = Instantiate(Prefab.gameObject, transform.position, transform.rotation) as GameObject;
        
        if(Bounds != null)
        {
            obj.GetComponent<StrategyCamera>().SetBounds(Bounds);
        }
    }
}
