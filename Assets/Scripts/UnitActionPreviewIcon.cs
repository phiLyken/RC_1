using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitActionPreviewIcon : UnitAction_TargetPreviewBase
{
    protected override void OnPreview(List<GameObject> objects)
    {
        List<Transform> targets = objects.Select(i => i.transform).ToList();
        UI_ActionPreviewIcon.PreviewOnTargets(targets, m_action.GetImage());
    }

    protected override void OnDisable()
    {
        UI_ActionPreviewIcon.Disable();
    }


}
 