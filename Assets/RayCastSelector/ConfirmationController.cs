using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmationController : MonoBehaviour {
	

	public bool WaitingForPlayerChoice;
	DialougeOptions currentChoice;
	
	public GameObject Window;
	public GameObject ConfirmButton;
	public GameObject DeclineButton;
	
	
	enum DialougeOptions{
		none, confirm, decline	
	}
	bool cancelled;
	public void CancelConfirmation(){
		Debug.Log("Cancelconfirmation");
		StopCoroutine("WaitForConfirmation");
		HideDialouge();
		cancelled = true;
	}
	public IEnumerator WaitForConfirmation( DialougeResult Result){
		cancelled = false;
		currentChoice = DialougeOptions.none;
		WaitingForPlayerChoice = true;
	
		DisplayDialouge();
		
		while(currentChoice == DialougeOptions.none && !cancelled){
		
			yield return null;	
		}
		
		Result.Success = currentChoice == DialougeOptions.confirm;
		Result.Waiting = false;
		HideDialouge();
	}
	
	public void DisplayDialouge(){
		Window.SetActive(true);
	}
	
	public void HideDialouge(){
		Window.SetActive(false);
	}
	
	public void Confirm(){
		if(!WaitingForPlayerChoice) return;
		currentChoice = DialougeOptions.confirm;
		WaitingForPlayerChoice = false;
	}
	
	public void Decline(){
		if(!WaitingForPlayerChoice) return;
		currentChoice = DialougeOptions.decline;
		WaitingForPlayerChoice = false;
	}
	
	public class DialougeResult{
		public bool Waiting = true;
		public bool Success;
	}
	
}
