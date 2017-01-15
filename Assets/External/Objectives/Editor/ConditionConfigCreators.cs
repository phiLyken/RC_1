using UnityEngine;
using UnityEditor;
using System.IO;

public static class ObjectiveConfigCreators
{
 
    public class CreateObjectiveConfig
    {
        [MenuItem("Assets/Create/Objectives/ObjectiveConfig")]
        public static void CreateAsset()
        {
           M_Math.ScriptableObjectUtility.CreateAsset<ObjectiveConfig>();
        }
    }
}


