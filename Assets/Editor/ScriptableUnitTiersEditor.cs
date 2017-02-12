using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ScriptableUnitTiers))]
public class ScriptableUnitTiersEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScriptableUnitTiers _target = target as ScriptableUnitTiers;
        List<IUnlockable> all_unlockables = new List<IUnlockable>();


        List < Unlockable < SquadSizeConfig> > squadsizes = UnlockableFactory.MakeUnlockables(_target.SquadSizes, null, SquadSizeConfig.GetRequirement, SquadSizeConfig.GetID);
        List <Unlockable<UnitTier>> units = UnlockableFactory.MakeUnlockables(  TieredUnit.AllTiers(_target.TieredUnitsSelectible), null, UnitTier.GetRequirement, UnitTier.GetID);

        all_unlockables.AddRange(units.Select( tier => tier as IUnlockable).ToList());
        all_unlockables.AddRange(squadsizes.Select(size => size as IUnlockable).ToList());

        all_unlockables = all_unlockables.OrderBy(unlockable => unlockable.GetLevelRequirement()).ToList();

        all_unlockables.ForEach(unlockable => EditorGUILayout.LabelField(unlockable.GetLevelRequirement()+" - "+ unlockable.GetID() ));
    }
}
