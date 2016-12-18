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


public class UnitMeshConfigCreator
{
    [MenuItem("Assets/Create/Unit Mesh Config")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<UnitMeshConfig>();
    }
}

public class LootConfigCreator
{
    [MenuItem("Assets/Create/LootConfig ")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<LootConfig>();
    }
}


public class ColorConfigCreator
{
    [MenuItem("Assets/Create/UI Color Config")]
    public static void CreateAsset()
    {
        MyMath.ScriptableObjectUtility.CreateAsset<UI_ActionBar_Button_ColorSetting>();
    }
}


public class CampConfigDatabase : ScriptableObject
{
    public MyMath.R_Range[] CampFrequenceByStage;

}