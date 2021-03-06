﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;

    Player[] Players;


    void Awake()
    {
        Players = GetComponents<Player>();
        Instance = this;
    }

    public static Player GetPlayer(int index) { 
    
        if(Instance == null) return null;

        return Instance.Players[index];
    }
}
