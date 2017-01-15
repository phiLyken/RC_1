using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericMonoSingleton <T> where T : Component {

    public static T Instance
    {
        get
        {
            return _instance == null ? createInstance() : _instance;
        }
    }
    static T _instance;

    static T createInstance()
    {
        GameObject new_go = new GameObject("_singleton_"+ typeof(T).Name);
        _instance = new_go.AddComponent<T>();
        return _instance;
    }
}
