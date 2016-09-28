using UnityEngine;
using System.Collections;

public static class UnitAnimationControllerHelper {

    /// <summary>
    /// thes animations will make the unit hide its weapon upon execution
    /// </summary>
    public static UnitAnimationTypes[] DisableWeapons =
    {
        UnitAnimationTypes.bHit, UnitAnimationTypes.bShooting
    };
}

 

public enum UnitAnimationTypes
{
    bHit, bRage, bLooting, bHealing, bShooting, bIntAttack, bDying
}