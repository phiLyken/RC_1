using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public static class GenericSingletonToPrefabMap
{
    public static string GetPrefab<T>()
    {

        switch (typeof(T).ToString())
        {
            case "PlayerInventory":
                return "player_inventory";

            case "SquadManager":
                return "squad_manager";

            case "GameManager":
                return "game_manager";

        }



        Debug.LogWarning("^lazysingleton: no prefab for " + typeof(T).ToString());
        return "NOT_FOUND";
       
      
    }

}

public interface IInit
{
    void Init();
}