using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


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

    public static  float GetDistance2D(Vector3 v1, Vector3 v2)
    {
        v1.y = 0;
        v2.y = 0;

        return (v1 - v2).magnitude;
    }

    public static Vector3 GetVectorInRange(Vector3 Vector, float _min, float _max){
		Debug.Log (GetPercentpointsOfValueInRange (Vector.magnitude, _min, _max));
		return Vector.normalized * GetPercentpointsOfValueInRange(Vector.magnitude, _min, _max);
	}

    public static T CloneMono<T>(T item)
    {
        return Instantiate((item as MonoBehaviour).gameObject).GetComponent<T>();
    }


    public static List<T> SpawnFromList<T>(List<T> list)
    {
        List<T> ret = new List<T>();
        foreach(var item in list)
        {
            ret.Add(CloneMono(item));
        }

        return ret;
       
    }
    public static Vector3 GetInputPos(){

     
                 return GetMouseWorldPos();
		
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


    public static int FloatDirection(float f)
    {
        return (int)(f / Mathf.Abs(f));
    }



    public static int GetSecondsNow()
    {
        return ConvertToUnixTimestamp(System.DateTime.Now);
    }

    public static System.DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        System.DateTime origin = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

    public static int ConvertToUnixTimestamp(System.DateTime date)
    {
        System.DateTime origin = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        System.TimeSpan diff = date.ToUniversalTime() - origin;
        return (int)(diff.TotalSeconds);
    }


    public static Vector3 GetInputPosToPlane()
    {

        if (Application.isEditor)
        {
            return GetMouseWorldPos();
        }
        else
        {
            return GetTouchWorldPos();
        }
    }

    public static Vector2 GetTouchMousePos()
    {
        if (Application.isEditor)
        {
            return Input.mousePosition;
        }
        else
        {
            return Input.touches[0].position;
        }
    }




    public static string GetStringFromSeconds(int seconds)
    {

        System.TimeSpan t = System.TimeSpan.FromSeconds(seconds);
        string timeText;
        int days = seconds / 86400;

        if (days >= 1)
        {
            string dayText = days > 1 ? "DAYS" : "DAY";
            timeText = string.Format("{0:D1} " + dayText + " - {1:D2}:{2:D2}:{3:D2}", t.Days, t.Hours, t.Minutes, t.Seconds);
        }
        else
        {
            timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
        return timeText;

    }


    public static bool StringArContains(string[] ar, string s)
    {
        for (int i = 0; i < ar.Length; i++) if (ar[i] == s) return true;

        return false;
    }

    public static float VectorDot01(Vector3 _in, Vector3 _out)
    {
        return (Vector3.Dot(_in, _out) * 0.5f) + 0.5f;
    }

    public static Vector2 Get2DForward(Transform tr)
    {
        return new Vector2(tr.forward.x, tr.forward.y);
    }

    public static Vector2 Get2DUP(Transform tr)
    {
        return new Vector2(tr.up.x, tr.up.y);
    }

    public static List<Transform> AddChildrenToList(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            children.Add(parent.GetChild(i));

        }

        return children;
    }

    public static T GetRandomObject<T>(List<T> objects)
    {      
        return objects[Random.Range(0, objects.Count)];
    }

    public static T GetRandomObject<T>(T[] objects)
    {
        return objects[Random.Range(0, objects.Length)];
    }

    /// <summary>
    /// Returns a certain amount of items randomly from a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static List<T> GetRandomObjects<T>(List<T> list, int num)
    {
        int count = Mathf.Min(num, list.Count);
        List<T> ret = new List<T>();	
        List<T> copy = new List<T>(list);

        for(int i = 0; i < count; i++)
        {
            T item = GetRandomObject(copy);
            copy.Remove(item);
            ret.Add(item);
        }

        return ret;
    }


    public static void FadeText(Text t, int cycles, Color Color1, Color Color2, float time1, float time2)
    {
        t.StartCoroutine(FadeTextSequence(t, cycles, Color1, Color2, time1, time2));
    }
    static IEnumerator FadeTextSequence(Text t, int cycles, Color Color1, Color Color2, float time1, float time2)
    {
        int m_cycles = cycles;
        while (cycles <= 0 || m_cycles >= 0)
        {

            yield return t.StartCoroutine(FadeTextOnce(t, Color1, time1));
            m_cycles--;
            if (cycles <= 0 || m_cycles > 0)
            {
                yield return t.StartCoroutine(FadeTextOnce(t, Color2, time2));
            }
            m_cycles--;
            yield return null;
        }
    }

    static IEnumerator FadeTextOnce(Text tf, Color targetColor, float t)
    {
        Color startcolor = tf.color;
        float time = 0;
        while (time < t)
        {
            Color newColor = Color.Lerp(startcolor, targetColor, time / t);
            tf.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public static float OrthogonalStrength(Vector2 _ref, Vector2 _in)
    {
        Vector2 perp = Vector3.Cross(_ref, new Vector3(0, 0, 1));
        return Vector2.Dot(perp, _in);

    }

    public static bool MouseTouchUp()
    {

        if (Application.isEditor)
        {
            return Input.GetMouseButtonUp(0);
        }
        else
        {
            return Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended);
        }
    }
    public static void SetListAsChild<T>(List<T> objects, Transform transform)
    {
        foreach (var item in objects)
        {
            (item as MonoBehaviour).transform.SetParent(transform);
        }
    }
    public static bool MouseTouchDown()
    {


        if (Application.isEditor)
        {
            return Input.GetMouseButtonDown(0);
        }
        else
        {
            return Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began);
        }
    }

    public static bool MouseTouchHold()
    {

        if (Application.isEditor)
        {
            return Input.GetMouseButton(0);
        }
        else
        {
            return Input.touchCount > 0;
        }
    }

    public static void CopyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;

    }

    /*https://gist.github.com/Arakade/9dd844c2f9c10e97e3d0*/

    public static void SceneViewText(string text, Vector3 worldPos, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();

        var view = UnityEditor.SceneView.currentDrawingSceneView;

		if(view != null){
	        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
	        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text)) * 1.5f;
	        GUI.Box(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height, size.x, size.y),"", GUI.skin.box );

	        if (colour.HasValue) GUI.color = colour.Value;
	        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height , size.x, size.y), text, UnityEditor.EditorStyles.whiteBoldLabel );
	        UnityEditor.Handles.EndGUI();

	        GUI.color = Color.white;
		}
    }

    public static string StringArrToLines(string[] str)
    {
        if (str == null || str.Length == 0) return "";
        string ret = "";
        foreach (string s in str) ret += s + "\n";
        return ret;
    }

    [System.Serializable]
    public class R_Range
    {
        public float min;
        public float max;
        public bool asInt;

        public float Value()
        {
            if (asInt)
            {
                return Random.Range((int)min, (int)max);
            } else { 
                return Random.Range(min, max);
            }
        }

        public R_Range(float _min, float _max)
        {
            asInt = false;
            min = _min;
            max = _max;
        }

        public R_Range(int _min, int _max)
        {
            asInt = true;
            min = _min;
            max = _max;
        }
    }

}
