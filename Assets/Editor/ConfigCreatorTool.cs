using UnityEngine;
using UnityEditor;
using System.IO;

public class CampConfigCreator
{
    [MenuItem("Assets/Create/Camp Config")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<CampConfigDatabase>();
    }
}


public class RegionConfigCreator
{
    [MenuItem("Assets/Create/Region Config")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<RegionConfig>();
    }
}

public class UnitConfigCreator
{
    [MenuItem("Assets/Create/Unit Config")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<ScriptableUnitConfig>();
    }
}


public class UnitMeshConfigCreator
{
    [MenuItem("Assets/Create/Unit Mesh Config")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<UnitMeshConfig>();
    }
}


public class UnitNameConfigCreator
{
    [MenuItem("Assets/Create/Unit Name Config")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<UnitNameConfig>();
    }
}

public class LootConfigCreator
{
    [MenuItem("Assets/Create/LootConfig ")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<LootConfig>();
    }
}


public class ColorConfigCreator
{
    [MenuItem("Assets/Create/UI Color Config")]
    public static void CreateAsset()
    { 
        M_Math.ScriptableObjectUtility.CreateAsset<UI_ActionBar_Button_ColorSetting>();
    }
}



public class SpeechTriggerSimpleCreator
{
    [MenuItem("Assets/Create/Unit Speech Trigger Simple")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<SpeechTriggerConfigSimple>();
    }
}


public class SpeechTriggerConfigCreator
{
    [MenuItem("Assets/Create/Unit Speech Trigger")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<SpeechTriggerConfig>();
    }
}


public class SpeechTriggerConfigIDCreator
{
    [MenuItem("Assets/Create/Unit Speech Trigger ID")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<SpeechTriggerConfigID>();
    }
}


public class SpeechConfigCreator
{
    [MenuItem("Assets/Create/Unit Speech Config")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<UnitSpeechConfig>();
    }
}

public class CampConfigDatabase : ScriptableObject
{
    public M_Math.R_Range[] CampFrequenceByStage;

}