using UnityEngine;
using System.Collections;

public class CameraGoalTest : MonoBehaviour {

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Camera.main.transform.parent.GetComponent<PanCamera>().PanToPos(transform.position);
        }
    }
}
