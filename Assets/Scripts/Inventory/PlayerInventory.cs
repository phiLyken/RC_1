﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : Inventory {

    public static PlayerInventory Instance;

    public override int GetMax(ItemTypes type)
    {
        return 99999999;
    }

    void Awake()
    {
        Instance = this;
    }
}


