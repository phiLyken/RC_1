using UnityEngine;
using System.Collections;

public class ObjectSelection : MonoBehaviour {
	
	 
	
	public LayerMask SelectibleLayers;
	
	//public Structure SelectedStructure;
	
	public static ObjectSelection Instance;
	public GameObject SelectedObject;
	public static bool IsSelected(GameObject obj){
		return obj == Instance.SelectedObject;	
	}
	GameObject LastTouchObject;

	Vector3 LastHitPosition;
	
	GameObject GetObjectFromTouch(){
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, SelectibleLayers)){
			LastHitPosition = hit.point;
			return hit.collider.gameObject;
		}
		
		return null;
	}
	
	public bool Cast(){
		
		LastTouchObject = GetObjectFromTouch();
		return LastTouchObject != null;
	}
	
	public void StartSelection(){
		MDebug.Log("Start Selection Mode");
	
		StartCoroutine(SelectionThread());	
	}
	
	void OnGUI(){
		GUI.Label( new Rect(0, 30, 200, 30), SelectedObject != null ? SelectedObject.name : "NONE");	
	}
	
	IEnumerator SelectionThread(){		
		GameObject ActiveObject = LastTouchObject;	
	
		
		while( Input.GetButton("Fire1")){
			if(RC_Camera.HasBlockingAction()) yield break;
			
			if(Cast()){
				LastTouchObject.SendMessage ("TouchHold", LastHitPosition, SendMessageOptions.DontRequireReceiver);					
			}
			ActiveObject = LastTouchObject;
			yield return null;
		}
		
		if(RC_Camera.HasBlockingAction()) yield break;
		
		MDebug.Log("Selection Thread End");
		if(ActiveObject != null ){
			MDebug.Log("Send Touchup to "+ActiveObject.name);
			ActiveObject.SendMessage ("TouchUp", SendMessageOptions.DontRequireReceiver);
		} else {
				UnselectCurrent();	
		}
		
		
	}
	

	void Awake(){
		Instance = this;
	}
	
	public void SetSelectedObject(GameObject newObject){
		
	
		
		if(SelectedObject == null){
			SelectedObject = newObject;
			SelectedObject.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);
			RC_Camera.Instance.DisableInput();
		} else {
			UnselectCurrent();	
		}
		                        
	}	
	
	public void UnselectObject(GameObject unselected){		
		
		if( SelectedObject == unselected ){
			SelectedObject = null;
		}
	}
	
	public void UnselectCurrent(){
		if(SelectedObject != null)
			SelectedObject.SendMessage("Unselect", SendMessageOptions.DontRequireReceiver);
	}
	

}
