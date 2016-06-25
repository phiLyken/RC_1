using UnityEngine;
using System.Collections;

public class UnitActionPreviewTiles : MonoBehaviour {

    public string StateID;
    MeshViewGroup previewed;


    void Awake()
    {
        UnitActionBase b = GetComponent<UnitActionBase>();

        b.OnSelectAction += OnSelect;
        b.OnUnselectAction += OnUnselect;
    }

    void OnSelect(UnitActionBase select)
    {
        previewed = new MeshViewGroup(select.GetPreviewTiles(), TileStateConfigs.GetMaterialForstate(StateID));
    }

    void OnUnselect(UnitActionBase unselect)
    {
        previewed.RemoveGroup();
        previewed = null;
    }
}

