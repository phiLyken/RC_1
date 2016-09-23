using UnityEngine;
using System.Collections;

public class MoveTileHighlight : MonoBehaviour {


    public GameObject TileSelectorPrefab;
    public Material WalkableMaterial;
    public Material NotWalkableMaterial;

    GameObject TileSelector;

    void Awake()
    {
        TileSelector = Instantiate(TileSelectorPrefab);
        TileSelector.SetActive(false);

        UnitAction_Move move = GetComponent<UnitAction_Move>();
        move.OnSetPreviewTile += SetToPosition;
        move.OnUnselectAction += OnUnSelectedMove;
    }


    void OnUnSelectedMove(UnitActionBase action)
    {
        TileSelector.SetActive(false);
    }

    void SetToPosition(Tile t, bool walkable)
    {
        TileSelector.SetActive(true);
        TileSelector.transform.position = t.GetPosition();

        TileSelector.GetComponentInChildren<MeshRenderer>().material = walkable ? WalkableMaterial : NotWalkableMaterial;
    }

 
}
