using UnityEngine;
using System.Collections;

public class CameraGoalTest : MonoBehaviour {

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           PanCamera.Instance.PanToPos(transform.position, ChangeColor );
        }
    }

    void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
