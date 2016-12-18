using UnityEngine;
using System.Collections;
using System.Linq;

public class UnitNameConfig : ScriptableObject {

    [TextArea(3, 10)]
    public string Names;

    [TextArea(3, 10)]
    public string Prefix;

    public string GetName()
    {
        return Prefix.Split(';').ToList().GetRandom()+" "+ Names.Split(';').ToList().GetRandom();
    }

}
