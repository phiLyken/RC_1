using UnityEngine;
using System.Collections;

public class UnitActionPreviewTiles : MonoBehaviour {

    
    MeshViewGroup previewed;

    protected UnitActionBase m_action;

    void Awake()
    {
        m_action = GetComponent<UnitActionBase>();

        m_action.OnSelectAction += OnSelect;
        m_action.OnUnselectAction += OnUnselect;
    }

    void OnSelect(UnitActionBase select)
    {
        previewed = new MeshViewGroup(select.GetPreviewTiles(), TileStateConfigs.GetMaterialForstate(m_action.GetTileViewState()));
    }

    void OnUnselect(UnitActionBase unselect)
    {
        previewed.RemoveGroup();
        previewed = null;
    }


}

