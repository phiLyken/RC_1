using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class RegionEditor : EditorWindow {
	
	public const string PATH = @"Assets/Regions/Resources/region_balancing_config.prefab";
	private RegionConfigDataBase RegionBalancing;

	[MenuItem("Foo/RegionBalancer %#w")]
	public static void Init(){
		RegionEditor window = EditorWindow.GetWindow<RegionEditor>();
		window.minSize= new Vector2(800,400);
		window.Show();
	}

	void OnEnable(){
		if(RegionBalancing == null) LoadRegionConfigs();

	}

	void OnGUI(){
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		if(GUILayout.Button("Add")){
			
		}

		EditorGUILayout.EndHorizontal();
	}


	void LoadRegionConfigs(){
		RegionBalancing = (RegionConfigDataBase) AssetDatabase.LoadAssetAtPath(PATH, typeof(RegionConfigDataBase));

		if(RegionBalancing == null){
			CreateRegionConfigs();
		}
	}

	void CreateRegionConfigs(){
		RegionBalancing = ScriptableObject.CreateInstance<RegionConfigDataBase>();

		AssetDatabase.CreateAsset(RegionBalancing, PATH);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

	}
}
