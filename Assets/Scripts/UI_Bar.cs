using UnityEngine;
using System.Collections;

public class UI_Bar : MonoBehaviour {

	int Width;
	public Transform Anchor;
	public float TestPercent;

	public Vector3 PixelOffset;

	public RectTransform Bar;
	
	CanvasRenderer mRenderer;
	
	float _percent = -1;

	public float Percent {
		set{
			if(Mathf.Clamp01(value) != _percent){
				_percent = Mathf.Clamp01(value);
				SetWidth();
			}
		}
		get {
			return _percent;
		}
	}

	RectTransform myTransform;

	void Awake(){
		mRenderer = GetComponent<CanvasRenderer>();
		myTransform = GetComponent< RectTransform >();
		
	}
	
	void Start(){
		Width = (int) Bar.sizeDelta.x;

		Anchor = transform.parent;
		transform.parent = null;
		 
		transform.parent = GameObject.FindGameObjectWithTag("HPBar").transform;	
	}
	
	void SetWidth(){

		if(_percent >= 1){
			mRenderer.SetAlpha(0);
		} else {
			mRenderer.SetAlpha(1);
		}
		Bar.sizeDelta =  new Vector2( Width * _percent, Bar.sizeDelta.y );
	}

	void Update(){
		
		if(Anchor == null){
			Destroy(gameObject);	
		} else {
			Vector3 Position = Camera.main.WorldToScreenPoint( Anchor.position);
			Position += PixelOffset;
			Position.x -= Width/2;
			myTransform.position = Position;
			TestPercent = Mathf.Clamp01(TestPercent);
		}
	}
}
