using UnityEngine;
using System.Collections;

public class PanCamera : MonoBehaviour {

    int currentZoomLevel;

	bool drag;
	Vector3 startDragPos;
	Vector3 smoothMove;
	float currentZoomDistance;
	int StopDragDelay = 4;
	bool zooming;
	bool inputEnabled;
    bool rotating;
    public static PanCamera Instance;

   // public GameObject CameraObj;
	
	public static bool CameraAction {
		get { return Instance != null &&( Instance.drag || Instance.zooming || Instance.rotating); }
	}
	
	public void DisableInput(){
	
		inputEnabled = false;
	}

	void Awake(){
		Instance = this;
	}
	
	void Start(){
		
		SetCameraStart();
	}
	void SetCameraStart(){
		currentZoomDistance = CameraDistanceToPlane();
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
	
	bool HasZoomInput()
    {
        return (Input.touchCount > 1 || Input.GetAxis("Mouse ScrollWheel") != 0);
    }
	IEnumerator ListenToZoomInput(){
		
		zooming = true;
		//Debug.Log("startZoom");
		float StartTouchDistance = Input.GetAxis("Mouse ScrollWheel") != 0 ? 0 : (Input.touches[0].position - Input.touches[1].position).magnitude;
		float LastTouchDistance = StartTouchDistance;
		float CurrentDeltaDistance = 0;
		float CurrentTouchDistance = 0;
		
		while (HasZoomInput()  && inputEnabled){
			
			if (Input.GetAxis("Mouse ScrollWheel") != 0) {
				CurrentTouchDistance =  Input.GetAxis("Mouse ScrollWheel") * -50;
			} else {
				CurrentTouchDistance =  (Input.touches[0].position - Input.touches[1].position).magnitude;
			}

			CurrentDeltaDistance = ( CurrentTouchDistance - LastTouchDistance );

			Debug.Log(CurrentDeltaDistance);

			LastTouchDistance =  CurrentTouchDistance;

			if(CurrentDeltaDistance != 0)
				SetZoomDistance( currentZoomDistance + CurrentDeltaDistance);
			yield return null;
			
		}
		
		StartCoroutine( DelayedStopZoom() );
        yield break;		
	}



	void SetZoomDistance(float dist){
		currentZoomDistance = Mathf.Clamp(dist, 1, 100);;
	}

    void ZoomCameraByDist(float dist)
    {
		
//		Debug.Log("Zoom "+dist);
        Vector3 newPos = transform.transform.position + (transform.transform.forward * dist);
       
        transform.transform.position = newPos;
        
    }

    public void SetZoomLevel(int newLevel)
    {
        currentZoomLevel = newLevel;
    }
    public static void FocusOnPlanePoint(Vector3 point)
    {
        if(Instance != null)
         Instance.PanToPos(point);
    }
    float GetDesiredCameraDistance()
    {
        return currentZoomDistance;
    }
    float CameraDistanceToPlane()
    {
        return (MyMath.GetPlaneIntersectionY(new Ray(transform.position, transform.forward)) - transform.position).magnitude;
    }

    float GetZoomDelta()
    {
        return GetDesiredCameraDistance() - CameraDistanceToPlane();
    }

    void Reset()
    {
        drag = false;
        rotating = false;
        zooming = false;
        StopAllCoroutines();
    }

    public void PanToPos(Vector3 pos)
    {

		drag = false;  
        StartCoroutine(PanToWorldPos(pos,5));
        
    }
	IEnumerator PanToWorldPos(Vector3 pos, float speed)
    {
       
        pos.y = 0;
        drag = true;
  
        Vector3 delta = pos -MyMath.GetCameraCenter() ;

       

        while ( delta.magnitude > 0.1f)
        {
            transform.Translate((pos - MyMath.GetCameraCenter()) * speed * Time.deltaTime, Space.World);
                       
            delta = pos - MyMath.GetCameraCenter();

            Debug.DrawLine(MyMath.GetCameraCenter(), pos, Color.red);
            yield return null;
        }

        drag = false;
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
			StartCoroutine(ListenToZoomInput());	
		}		                                            
		 
		yield break;
		
	}
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, MyMath.GetCameraCenter());
    }

    IEnumerator Rotate(float step)
    {
        if (rotating) yield break;
        rotating = true;
        float rotated = 0;
        float t = 0;

        while (Mathf.Abs(rotated) < Mathf.Abs( step)) {

            float target_rot = Mathf.Lerp(0, step, t);
            float dr = target_rot - rotated;
            rotated += dr;
            t += Time.deltaTime * 3;
            transform.RotateAround(MyMath.GetCameraCenter(), Vector3.up, dr);
            yield return null;
        }

        transform.RotateAround(MyMath.GetCameraCenter(), Vector3.up, rotated- step);


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
	


	IEnumerator DelayedStopDrag(){
		for(int i = 0; i < StopDragDelay;i++) yield return new WaitForEndOfFrame();
		
		drag = false;
	}
	
	IEnumerator DelayedStopZoom(){
		for(int i = 0; i < StopDragDelay;i++) yield return new WaitForEndOfFrame();
		
		zooming = false;
	}

}
