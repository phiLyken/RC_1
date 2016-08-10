using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum ArmorParts
{
    head, torso, legs
}

[System.Serializable]
public class ArmorPartConfig
{
    public ArmorParts part;
    public GameObject Mesh;
}
public class ArmorConfig : ScriptableObject {

    public List<ArmorPartConfig> Parts;


}
