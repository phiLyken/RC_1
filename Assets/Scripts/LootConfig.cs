using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LootConfig : ScriptableObject
{
    public LootCategory Category;
    public GameObject WorldObject; 
    public List<LootContentConfig   > Drops;
}
