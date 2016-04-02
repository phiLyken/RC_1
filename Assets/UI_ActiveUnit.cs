using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ActiveUnit : MonoBehaviour {
    public  Text SelectedUnitTF;
    public Text AbilityTF;

    public static UI_ActiveUnit Instance;
    void Awake()
    {
        Instance = this;
    }
}
