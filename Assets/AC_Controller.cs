using UnityEngine;
using System.Collections;

public class AC_Controller : MonoBehaviour {
	
	public IEnumerator CamInputControl(){

		//		Debug.Log(" Cam Input Start");
		inputEnabled = true;
		startDragPos =  MyMath.GetInputPos();

		while(inputEnabled && (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0) ){

			Vector3 mousePos = MyMath.GetInputPos();			

			if (Input.touchCount > 1 || Input.GetAxis("Mouse ScrollWheel") != 0){				
				StartCoroutine(ListenToZoomInput());
				yield break;
			}

			if((startDragPos - mousePos).magnitude > 0.5f){
				StartCoroutine(Pan ());						
				yield break;
			} 



			yield return null;
		}
	}

	IEnumerator MobileZoom(){
		float StartTouchDistance = (Input.touches[0].position - Input.touches[1].position).magnitude;
		float LastTouchDistance = StartTouchDistance;
		float CurrentDeltaDistance = 0;
		float CurrentTouchDistance = 0;

		while ( Input.touches.Length > 1 ){
			CurrentTouchDistance =  (Input.touches[0].position - Input.touches[1].position).magnitude;
			CurrentDeltaDistance = ( CurrentTouchDistance - LastTouchDistance );

			ZoomBy( CurrentDeltaDistance );
		}
	}

	IEnumerator ListenToZoomInput(){

	
		//Debug.Log("startZoom");


		while (HasZoomInput()  && inputEnabled){

			if (Input.GetAxis("Mouse ScrollWheel") != 0) {
				CurrentTouchDistance =  Input.GetAxis("Mouse ScrollWheel") * -50;
			} else {
				
			}



			//			Debug.Log(CurrentDeltaDistance);

			LastTouchDistance =  CurrentTouchDistance;

			if(CurrentDeltaDistance != 0)
				
			yield return null;

		}

		StartCoroutine( DelayedStopZoom() );
		yield break;		
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(Rotate(-45));
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			StartCoroutine(Rotate(45));
		}

		if ( !CameraAction && ( Input.GetMouseButtonDown(0) || Input.touchCount > 0 || HasZoomInput() ) )
		{
			StartCoroutine(CamInputControl());
		}

		if(!CameraAction && smoothMove.magnitude > 0){
			transform.Translate(-smoothMove, Space.World);
			smoothMove = Vector3.Lerp(smoothMove, Vector3.zero, Time.deltaTime* 10 );
		}

		if(GetZoomDelta() != 0){
			ZoomCameraByDist( - GetZoomDelta() * Time.deltaTime * 5);
		}
	}
}
