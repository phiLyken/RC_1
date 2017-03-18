using UnityEngine;
using System.Collections;

public class MDebug  {

    static bool enabled = false;

    public static void Log(string log)
    {
        if (!enabled)
            return;
#if UNITY_EDITOR
        Debug.Log(log);
#endif
    }

    public static void LogWarning(string log)
    {
        if (!enabled)
            return;
#if UNITY_EDITOR
        Debug.LogWarning(log);
    #endif
    }

    public static void LogError(string log)
    {
        if (!enabled)
            return;
#if UNITY_EDITOR
        Debug.LogError(log);
#endif
    }
}
