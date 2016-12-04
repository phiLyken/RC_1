using UnityEngine;
using System.Collections;

public class InitCameraSpawner : MonoBehaviour {

    public Camera Prefab;
    public void Awake()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation) as GameObject;
    }
}
