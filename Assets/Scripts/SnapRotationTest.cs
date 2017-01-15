using UnityEngine;
using System.Collections;

public class SnapRotationTest : MonoBehaviour {

    public Transform target;
    public float snap;
    public float speed;
	// Use this for initialization
	void Start () {
        StartCoroutine(Rotate());
	}
	
	// Update is called once per frame
	void Update () {
       // transform.rotation = Quaternion.RotateTowards( transform.rotation, MyMath.RotateToYSnapped(transform.position, target.transform.position, snap), speed);
    }

    IEnumerator Rotate()
    {
        yield return new WaitForRotation(transform, M_Math.RotateToYSnapped(transform.position, target.transform.position, snap), speed);

    }

}
