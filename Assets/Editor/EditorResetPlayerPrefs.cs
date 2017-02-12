using UnityEngine;
using System.Collections;
using UnityEditor;

public static class EditorResetPlayerPrefs  {
    [MenuItem("Foo/ResetPlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }




}
