using UnityEngine;
using System.Collections;

public class SimplePan : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        Vector3 InputDir = new Vector3(x, 0, z);
        Vector3 AdjustedInputDir = transform.TransformDirection(InputDir);
        AdjustedInputDir.y = 0;
        AdjustedInputDir.Normalize();

        transform.Translate(AdjustedInputDir * Time.deltaTime * 10, Space.World);
        Debug.DrawRay(transform.position, AdjustedInputDir * 5, Color.red);
	}
}
