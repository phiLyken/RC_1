using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class LazySingleton <T> where T : MonoBehaviour {

    

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
        GameObject new_go = Resources.Load(GenericSingletonToPrefabMap.GetPrefab<T>()).Instantiate(null, false);
        ;
        _instance = new_go.GetComponent<T>();
        return _instance;
    }

    protected abstract void Init();
}

public static class GenericSingletonToPrefabMap
{
    public static string GetPrefab<T>()
    {

        switch (typeof(T).ToString())
        {
            case "PlayerInventory":
                return "player_inventory";
           

        }



        Debug.LogWarning("^lazysingleton: no prefab for " + typeof(T).ToString());
        return "NOT_FOUND";
       
      
    }

}