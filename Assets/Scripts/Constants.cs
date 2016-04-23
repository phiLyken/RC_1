using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
    
    /// <summary>
    /// for each intensity point, nore damage is received
    /// </summary>
    public static float DAMAGE_RECEIVED_PER_INT = 0.0f;

    /// <summary>
    /// How close a player unit must come before the an enemy AI is activated and added to the turn system
    /// </summary>
    public static float UNIT_ACTIVATION_RANGE = 8;

    /// <summary>
    /// For each point of will, intensity received is decreased by _x_ points
    /// </summary>
    public static float WILL_TO_INT = 0.0f;

    /// <summary>
    /// Foreach point of damage a unit gets, it will receive some _x_ points of int
    /// </summary>
    public static float RCV_DMG_TO_INT = 1;

    /// <summary>
    /// Amount of will restored per int when using rest ability
    /// </summary>
    public static float INT_TO_HEAL = 1f;

    /// <summary>
    /// Amount of Extra Damage Dealt per Point of Intensity
    /// </summary>
    public static float INT_TO_DMG = 1.0f;


    public static float IncomingDamageModifier(float intensity)
    {
        return 1 + intensity * DAMAGE_RECEIVED_PER_INT;
    }



}
