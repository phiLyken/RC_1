using UnityEngine;
using System.Collections;

public class MyMath : MonoBehaviour {
	
	public static GameObject GetClosestGameObject(Vector3 originPosition, GameObject[] objects){
		
		if( objects.Length == 0) return null;
		
		GameObject best = objects[0];
	
		float closestDistance = Vector3.Magnitude( best.transform.position - originPosition);

		for(int i = 1 ; i < objects.Length; i++){
			float currentDistance = Vector3.Magnitude( objects[i].transform.position - originPosition);
			if(currentDistance < closestDistance){
				best = objects[i];
				closestDistance = currentDistance;
			}
		}
		
		return best;
	}
	public static float GetPercentpointsOfValueInRange(float _value, float _min, float _max){
		if (_value < _min)
						return 0;
		if (_value > _max)
						return 1;
	
		return (_value - _min) / (_max - _min);

	}

	public static Vector3 GetVectorInRange(Vector3 Vector, float _min, float _max){
		Debug.Log (GetPercentpointsOfValueInRange (Vector.magnitude, _min, _max));
		return Vector.normalized * GetPercentpointsOfValueInRange(Vector.magnitude, _min, _max);
	}
	public static Vector3 GetInputPos(){
		
		if(Application.isEditor){
			return GetMouseWorldPos();	
		} else {
			return GetTouchWorldPos();
		}
		
	}
	public static Vector3 GetTouchWorldPos(){
		Vector3 pos = Vector3.zero;
		
		if( Input.touchCount > 0 ){
			pos = new Vector3( Input.touches[0].position.x, Input.touches[0].position.y, 0);
			return GetPlaneIntersectionY(Camera.main.ScreenPointToRay(pos));
		}
		return pos;
	}
	public static Vector3 GetMouseWorldPos(){	
		return GetPlaneIntersectionY(Camera.main.ScreenPointToRay(Input.mousePosition));		
	}
	public static Vector3 GetCameraCenter()
    {
        return GetPlaneIntersectionY(new Ray(Camera.main.transform.position,Camera.main.transform.forward));
    }
	public static Vector3 GetPlaneIntersectionY(Ray ray){
		
		float dist = Vector3.Dot (Vector3.up, Vector3.zero - ray.origin) / Vector3.Dot (Vector3.up, ray.direction.normalized);
		
		return ray.origin + ray.direction.normalized * dist;
		
		
	}

    public static void DeleteChildren(GameObject obj)
    {
  
        for(int i = obj.transform.childCount-1; i >= 0; i--)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
}
