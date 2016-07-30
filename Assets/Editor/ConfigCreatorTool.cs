using UnityEngine;
using UnityEditor;
using System.IO;

public class CampConfigCreator
{
    [MenuItem("Assets/Create/Camp Config")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<CampConfigDatabase>();
    }
}


public class RegionConfigCreator
{
    [MenuItem("Assets/Create/Region Config")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<RegionConfig>();
    }
}

public class UnitConfigCreator
{
    [MenuItem("Assets/Create/Unit Config")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<ScriptableUnitConfig>();
    }
}

public class Generic_ItemCreator
{
    [MenuItem("Assets/Create/Generic_Item")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<Item_Generic>();
    }
}



public class UnitConfigUtility
{
    [MenuItem("Assets/Create/Unit Copy")]
    public static void CreateAsset()
    {
        foreach (UnitConfig conf in UnitConfigsDatabase.GetAllConfigs())
        {
            CreateAsset(conf);
        }
        
    }

  

    public static ScriptableUnitConfig CreateAsset(UnitConfig conf) 
    {
        ScriptableUnitConfig asset = ScriptableObject.CreateInstance<ScriptableUnitConfig>();
        asset.CopyFromConfig(conf);

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/"+conf.ID+".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        return asset;
    }
}


public class CampConfigDatabase : ScriptableObject
{
    public MyMath.R_Range[] CampFrequenceByStage;

}