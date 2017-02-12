using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelConfigCreater
{
    [MenuItem("Assets/Create/LevelConfig")]
    public static void CreateAsset()
    {
        M_Math.ScriptableObjectUtility.CreateAsset<ScriptableLevelConfig>();
    }
}