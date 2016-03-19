﻿using UnityEngine;
using System.Collections;

public class PanCamera : MonoBehaviour {

	bool drag;
	Vector3 startDragPos;
	Vector3 smoothMove;
	int StopDragDelay = 4;
	bool zooming;
	bool inputEnabled;
	
	public GameObject CameraObj;
	
	public static bool CameraAction {
		get { return Instance.drag || Instance.zooming; }
	}
	
	public void DisableInput(){
	
		inputEnabled = false;
	}
	public static PanCamera Instance;
	void Awake(){
		Instance = this;
	}
	
	void Start(){
		
		SetCameraStart();
	}
	void SetCameraStart(){
       
	}
        
	
	void OnGUI(){
		GUI.Label( new Rect(0,0,100,30), zooming ? "Zooming" : (drag ? "Panning" : " ")+ "    touches:"+Input.touchCount);
	}
	public IEnumerator CamInputControl(){
		
		//		Debug.Log(" Cam Input Start");
		inputEnabled = true;
		startDragPos =  MyMath.GetInputPos();
		
		while(inputEnabled && (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0) ){
			
			Vector3 mousePos = MyMath.GetInputPos();			
			
			if (Input.touchCount > 1 || Input.GetAxis("Mouse ScrollWheel") != 0){				
				StartCoroutine(Zooming());
				yield break;
			}
			
			if((startDragPos - mousePos).magnitude > 0.5f){
				StartCoroutine(Pan ());						
				yield break;
			} 
			
			
			
			yield return null;
		}
	}
	
	bool HasZoomInput()
    {
        return (Input.touchCount > 1 || Input.GetAxis("Mouse ScrollWheel") != 0);
    }
	IEnumerator Zooming(){
		
		zooming = true;
		Debug.Log("startZoom");
		float StartTouchDistance = Input.GetAxis("Mouse ScrollWheel") != 0 ? 0 : (Input.touches[0].position - Input.touches[1].position).magnitude;
		float LastTouchDistance = StartTouchDistance;
		float CurrentDeltaDistance = 0;
		float CurrentTouchDistance = 0;
		
		while (HasZoomInput()  && inputEnabled){
			if (Input.GetAxis("Mouse ScrollWheel") != 0) {
				CurrentTouchDistance =  Input.GetAxis("Mouse ScrollWheel") * 1200;
			} else {
				CurrentTouchDistance =  (Input.touches[0].position - Input.touches[1].position).magnitude;
			}
			
			CurrentDeltaDistance = ( CurrentTouchDistance - LastTouchDistance );
		
			LastTouchDistance =  CurrentTouchDistance;
			
			Vector3 newPos = CameraObj.transform.position + (CameraObj.transform.forward * CurrentDeltaDistance / 75);
			if (newPos.y > 1 && newPos.y < 20){
				CameraObj.transform.position = newPos;
			}
			yield return null;
			
		}
		
		StartCoroutine( DelayedStopZoom() );
		

		yield break;
		
	}

	
	IEnumerator Pan(){
		drag = true;
    
		startDragPos = MyMath.GetInputPos();
		while( inputEnabled && (Input.touchCount == 1 || Input.GetMouseButton(0))){
			
			Vector3 mousePos = MyMath.GetInputPos();
			Vector3 delta = mousePos - startDragPos;
			smoothMove = delta * Time.deltaTime * 10;
			transform.position =  transform.position - smoothMove;
			yield return null;
		}
		
		StartCoroutine(DelayedStopDrag());
		
		
		if(inputEnabled && (Input.touchCount == 2)){
			smoothMove *= 0.2f;			
			StartCoroutine(Zooming());	
		}		                                            
		 
		yield break;
		
	}
	// Update is called once per frame
	void Update () {
			
		if (Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyDown(KeyCode.Escape)){
			SetCameraStart();	
		}
		
        if( !CameraAction && ( Input.GetMouseButtonDown(0) || Input.touchCount > 0 || HasZoomInput() ) )
        {
            StartCoroutine(CamInputControl());
        }
	
		if(!CameraAction && smoothMove.magnitude > 0){
			transform.Translate(-smoothMove, Space.World);
			smoothMove = Vector3.Lerp(smoothMove, Vector3.zero, Time.deltaTime* 10 );
		}
	}
	


	IEnumerator DelayedStopDrag(){
		for(int i = 0; i < StopDragDelay;i++) yield return new WaitForEndOfFrame();
		
		drag = false;
	}
	
	IEnumerator DelayedStopZoom(){
		for(int i = 0; i < StopDragDelay;i++) yield return new WaitForEndOfFrame();
		
		zooming = false;
	}

}
