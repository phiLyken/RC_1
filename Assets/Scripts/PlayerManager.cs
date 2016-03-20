using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;

    Player[] Players;


    void Awake()
    {
        Players = GetComponents<Player>();
    }

    public static Player GetPlayer(int index)
    {
        return Instance.Players[index];
    }
}
