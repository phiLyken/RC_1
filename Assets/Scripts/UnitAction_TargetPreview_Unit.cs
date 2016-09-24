using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_TargetPreview_Unit : UnitAction_TargetPreview<Unit>
{

    protected override Vector3 GetPreviewPosition(object tgt)
    {
        return (tgt as Unit).currentTile.GetPosition();
    }
}

