using UnityEngine;
using System.Collections;

public class ConfirmationTester : MonoBehaviour {
	
	public ConfirmationController Test;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Fire2") && !Test.WaitingForPlayerChoice){
			StartCoroutine( Confirm());
		}
	}
	
	IEnumerator Confirm(){
		
		ConfirmationController.DialougeResult result = new ConfirmationController.DialougeResult();
		
		yield return StartCoroutine( Test.WaitForConfirmation( 	result ));
		
		MDebug.Log("CONFIRMED: "+result.Success);
	}
}
