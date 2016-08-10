using UnityEngine;
using System.Collections;

public static class UnitAnimationControllerHelper {

    /// <summary>
    /// thes animations will make the unit hide its weapon upon execution
    /// </summary>
    public static OneShotAnimations[] DisableWeapons =
    {
        OneShotAnimations.get_hit, OneShotAnimations.shoot
    };
}



/// <summary>
/// The must be in the same order as weapon states in the controller
/// </summary>
public enum WeaponAnimationStates
{
    one, two, three, four
};

/// <summary>
/// These should have the same name as the trigger in the animation controller
/// </summary>
public enum OneShotAnimations
{
    get_hit, shoot, heal
};
