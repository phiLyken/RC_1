using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

 
public class UnitAction_TargetPreview_Tile : UnitAction_TargetPreview<Tile>
{


    protected override Vector3 GetPreviewPosition(object tgt)
    {
        return (tgt as Tile).GetPosition();
    }
}
