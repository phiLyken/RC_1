using UnityEngine;
using System.Collections;

public class ToastNotifactionTest : MonoBehaviour {


	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            ToastNotification.SetToastMessage1("TOAST  111111!!!!");
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            ToastNotification.SetToastMessage2("TOAST 22222!!!!");
        }
    }
}
