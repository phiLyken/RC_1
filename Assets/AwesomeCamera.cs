using UnityEngine;
using System.Collections;

public class AwesomeCamera : MonoBehaviour {

	float CameraDistanceToPlane()
	{
		return (MyMath.GetPlaneIntersectionY(new Ray(transform.position, transform.forward)) - transform.position).magnitude;
	}

	void SetCameraStart(){
		currentZoomDistance = CameraDistanceToPlane();
	}

}

class CameraAction : MonoBehaviour {
	
	public bool InProgress;

	void Update(){

	}
}

class CameraAction_Move : CameraAction {

	IEnumerator DelayedStopDrag(){
		for(int i = 0; i < StopDragDelay;i++) yield return new WaitForEndOfFrame();

		drag = false;
	}

	IEnumerator DelayedStopZoom(){
		for(int i = 0; i < StopDragDelay;i++) yield return new WaitForEndOfFrame();

		zooming = false;
	}

	IEnumerator MoveToWorldPosition(Vector3 pos, float speed)
	{

		pos.y = 0;
		yield return null;
		Vector3 delta = pos - MyMath.GetCameraCenter() ;

		while (delta.magnitude > 0.1f)
		{
			transform.Translate((pos - MyMath.GetCameraCenter()) * speed * Time.deltaTime, Space.World);

			delta = pos - MyMath.GetCameraCenter();

			Debug.DrawLine(MyMath.GetCameraCenter(), pos, Color.red);
			yield return null;
		}
	}

	public void PanToPos(Vector3 pos)
	{
		StartCoroutine(PanToWorldPos(pos,5));
	}

	IEnumerator Pan(){
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


}

class CameraAction_Rotate : CameraAction {
	IEnumerator Rotate(float step)
	{

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

	}
}

class CameraAction_Zoom : CameraAction {

	float currentZoomDistance;

	void SetZoomDistance(float dist){
		currentZoomDistance = Mathf.Clamp(dist, 1, 100);;
	}

	void ZoomCameraByDist(float dist)
	{
		//		Debug.Log("Zoom "+dist);
		Vector3 newPos = transform.transform.position + (transform.transform.forward * dist);
		transform.transform.position = newPos;

	}


	float GetDesiredCameraDistance()
	{
		return currentZoomDistance;
	}

	float GetZoomDelta()
	{
		return GetDesiredCameraDistance() - CameraDistanceToPlane();
	}

}