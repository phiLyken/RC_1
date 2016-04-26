using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class UnitEditor : EditorWindow {
	
	private enum State
	{
		BLANK,
		EDIT,
		ADD
	}

	private State state;

	private const string PATH = @"Assets/Units/unit_config.asset";
	private UnitConfigs Units;

	[MenuItem("Foo/UnitEditor %#w")]
	public static void Init(){
		UnitEditor window = EditorWindow.GetWindow<UnitEditor>();
		window.minSize= new Vector2(800,400);
		window.Show();
	}

	void OnEnable(){
		if(Units == null) LoadUnitConfigs();
		state = State.BLANK;
	}

	void OnGUI(){
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		if(GUILayout.Button("Add")){
			Units.Add( new UnitConfig("Bla "+UnityEngine.Random.Range(0,100000)));
		}

		EditorGUILayout.EndHorizontal();
	}

	void LoadUnitConfigs(){
		Units = (UnitConfigs) AssetDatabase.LoadAssetAtPath(PATH, typeof(UnitConfigs));

		if(Units == null){
			CreateUnitConfig();
		}
	}

	void CreateUnitConfig(){
		Units = ScriptableObject.CreateInstance<UnitConfigs>();

		AssetDatabase.CreateAsset(Units, PATH);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

	}

}