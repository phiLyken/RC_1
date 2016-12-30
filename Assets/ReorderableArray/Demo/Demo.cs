using UnityEngine;
using System;
using System.Collections.Generic;

public class Demo : MonoBehaviour {

	[Header("Reorderable Arrays and Lists")]
	public Vector3[] vectorArray;
	public List<Color> colorList;
	public Texture2D[] textureArray;

	[Header("Range Value Reorderable")]
	[Range(1, 10)]
	public int[] intArray;

	[Header("Classic Arrays")]
	[Range(1, 10)]
	[NotReorderable]
	public float[] classicFloatArray;
	[NotReorderable]
	public Texture2D[] classicTextureArray;
	[NotReorderable]
	public Vector3[] classicVector3Array;
 
	[Header("Custom Drawer Tests")]
	public List<MagicSpell> customDrawers;

 
    public List<SpeechTriggerConfig> speeches;
}

 
[Serializable]
public class MagicSpell {
	public string name;
	public UnityEngine.Object target;
    public List<Vector3> bla;
}

