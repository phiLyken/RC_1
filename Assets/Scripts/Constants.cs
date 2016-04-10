using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
    
    /// <summary>
    /// multliplier is calculated by 1 + INT(damage receiver) * INT_TO_DMG;
    /// </summary>
    public static float INT_TO_DMG_RCV = 1;

    /// <summary>
    /// How close a player unit must come before the an enemy AI is activated and added to the turn system
    /// </summary>
    public static float UNIT_ACTIVATION_RANGE = 8;

    /// <summary>
    /// For each point of will, intensity received is decreased by _x_ points
    /// </summary>
    public static float WILL_TO_INT = -0.1f;

    /// <summary>
    /// Foreach point of damage a unit gets, it will receive some _x_ points of int
    /// </summary>
    public static float RCV_DMG_TO_INT = 1;

    /// <summary>
    /// Amount of will restored per int when using rest ability
    /// </summary>
    public static float INT_TO_HEAL = 0.5f;

   
}
