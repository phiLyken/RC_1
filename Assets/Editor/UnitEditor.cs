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
	private UnitConfigsDatabase Units;

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
		Units = (UnitConfigsDatabase) AssetDatabase.LoadAssetAtPath(PATH, typeof(UnitConfigsDatabase));

		if(Units == null){
			CreateUnitConfig();
		}
	}

	void CreateUnitConfig(){
		Units = ScriptableObject.CreateInstance<UnitConfigsDatabase>();

		AssetDatabase.CreateAsset(Units, PATH);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

	}

}