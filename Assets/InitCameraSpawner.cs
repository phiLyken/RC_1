using UnityEngine;
using System.Collections;

public class InitCameraSpawner : MonoBehaviour {

    public Camera Prefab;
    public void Awake()
    {
        GameObject obj = Instantiate(Prefab.gameObject, transform.position, transform.rotation) as GameObject;
    }
}
