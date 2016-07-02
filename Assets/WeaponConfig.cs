using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponConfig : MonoBehaviour
{

    public string ID;
    public Sprite Icon;

    public int Range;

    public WeaponBehavior RegularBehavior;
    public WeaponBehavior IntAttackBehavior;

}
