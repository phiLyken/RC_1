﻿using UnityEngine;
using System.Collections;

public class PanCamera : MonoBehaviour {

 

    int currentZoomLevel;
	bool drag;
	Vector3 startDragPos;
	Vector3 smoothMove;
	int StopDragDelay = 4;
	bool zooming;
	bool inputEnabled;
    bool rotating;

    EventHandler event_callback;

    public static PanCamera Instance;

   // public GameObject CameraObj;
	
	public static bool CameraAction {
		get { return Instance != null &&( Instance.drag || Instance.zooming || Instance.rotating); }
	}
	
	public void DisableInput(){
	
		inputEnabled = false;
	}

    void Awake() {
        Instance = this;
        Unit.OnTurnStart += OnTurnStart;

    }

    public void OnDestroy()
    {
        Unit.OnTurnStart -= OnTurnStart;
    }
    void OnTurnStart(Unit u)
    {
        PanToPos(u.transform.position);
    }

	void Start(){    
		SetCameraStart();
	}
	void SetCameraStart(){
       
	}

	void OnGUI(){
		//GUI.Label( new Rect(0,0,100,30), zooming ? "Zooming" : (drag ? "Panning" : " ")+ "    touches:"+Input.touchCount);
	}
	public IEnumerator CamInputControl(){
		
		//		MDebug.Log(" Cam Input Start");
		inputEnabled = true;
		startDragPos =  M_Math.GetInputPos();
		
		while(inputEnabled && (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0) ){
			
			Vector3 mousePos = M_Math.GetInputPos();			
			
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
		//MDebug.Log("startZoom");
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

            Zoom(CurrentTouchDistance / 75);
			yield return null;			
		}
		
		StartCoroutine( DelayedStopZoom() );
        yield break;		
	}

    void Zoom(float dist)
    {
        Vector3 newPos = transform.transform.position + (transform.transform.forward * dist);
        if (newPos.y > 1 && newPos.y < 20)
        {
            transform.transform.position = newPos;
        }
    }

    public void SetZoomLevel(int newLevel)
    {
        currentZoomLevel = newLevel;
    }

    float GetDesiredCameraDistance()
    {
        return currentZoomLevel * 5;
    }
    float CameraDistanceToPlane()
    {
        return (M_Math.GetPlaneIntersectionY(new Ray(transform.position, transform.forward)) - transform.position).magnitude;
    }

    float GetZoomDelta()
    {
        return GetDesiredCameraDistance() - CameraDistanceToPlane();
    }

    void Reset()
    {
        if (event_callback != null)
        { 
            event_callback();
            event_callback = null;
        }


     
        drag = false;
        rotating = false;
        zooming = false;
        StopAllCoroutines();
    }
    public void PanToPos(Vector3 pos)
    {

        PanToPos(pos, null);
    }

 
    public void PanToPos(Vector3 pos, EventHandler cb)
    {

        DisableInput();
        Reset();
        StartCoroutine(PanToWorldPos(pos, 20,cb));
    }

	IEnumerator PanToWorldPos(Vector3 pos, float speed, EventHandler _cb)
    {
        MDebug.Log("Start Pan");
        pos.y = 0;
        drag = true;
        event_callback = _cb;
        Vector3 delta = pos -M_Math.GetCameraCenter() ;     

        while ( delta.magnitude > 0.1f)
        {
            transform.Translate((pos - M_Math.GetCameraCenter()).normalized * speed * Time.deltaTime, Space.World);
                       
            delta = pos - M_Math.GetCameraCenter();

            Debug.DrawLine(M_Math.GetCameraCenter(), pos, Color.red);
            yield return null;
        }

        if (event_callback != null)
        {
            event_callback();
            event_callback = null;
        }

        inputEnabled = true;
        drag = false;
    }

	IEnumerator Pan(){
		drag = true;
    
		startDragPos = M_Math.GetInputPos();
		while( inputEnabled && (Input.touchCount == 1 || Input.GetMouseButton(0))){
			
			Vector3 mousePos = M_Math.GetInputPos();
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


    IEnumerator Rotate(float delta)
    {
        if (rotating) yield break;
        rotating = true;
        float rotated = 0;
        float t = 0;

        while (Mathf.Abs(rotated) < Mathf.Abs( delta)) {

            float target_rot = Mathf.Lerp(0, delta, t);
            float dr = target_rot - rotated;
            rotated += dr;
            t += Time.deltaTime * 3;
            transform.RotateAround(M_Math.GetCameraCenter(), Vector3.up, dr);
            yield return null;
        }

        transform.RotateAround(M_Math.GetCameraCenter(), Vector3.up, rotated- delta);


        rotating = false;
    }
   
	void Update () {
			
		if (Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyDown(KeyCode.Escape)){
			SetCameraStart();	
		}
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Rotate(-45));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Rotate(45));
        }

        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !CameraAction && ( Input.GetMouseButtonDown(0) || Input.touchCount > 0 || HasZoomInput() ) )
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
