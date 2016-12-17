using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[System.Serializable]
public class TargetInfo
{
    public bool TargetFriendly;
    public bool TargetEnemies;
    public bool TargetSelf;

    public float Range;
    public UseStat Modifier;

    public float GetRange(Unit for_unit)
    {
        return Range + Modifier.GetValue(for_unit);
    }

    /// <summary>
    /// Returns true if the target is targetable (according to the specified targeting rules)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="fromTile"></param>
    /// <returns></returns>
    public static bool CanTarget(TargetInfo targetRules, Unit targeter, Unit target, Tile fromTile)
    {
        if (target == targeter && !targetRules.TargetSelf)
            return false;
        if (!targetRules.TargetSelf && (!targetRules.TargetFriendly && target.OwnerID == targeter.OwnerID))
            return false;
        if (!targetRules.TargetEnemies && target.OwnerID != targeter.OwnerID)
            return false;
        if (!IsInRangeAndHasLOS(targeter, target, targetRules.GetRange(targeter), fromTile))
            return false;

        return true;

    }

    public static bool IsInRangeAndHasLOS(Unit instigator, Unit target, float range)
    {
        return IsInRangeAndHasLOS(instigator, target, range, instigator.currentTile);
    }


    public static bool IsInRangeAndHasLOS(Unit instigator, Unit target, float range, Tile origin)
    {
        List<Tile> in_range = LOSCheck.GetTilesVisibleTileInRange(origin, (int) range);

        return in_range.Contains(target.currentTile);
    }
}