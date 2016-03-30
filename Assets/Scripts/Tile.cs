using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public struct TilePos
{
    public TilePos(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public int x;
    public int z;
}

[RequireComponent(typeof(BoxCollider), typeof(SelectibleObjectBase))]
public class Tile : MonoBehaviour, IWayPoint
{
    public int zoneID;
    public int CrumbleDelay;
   
    public int MinLifeTime;

    public bool isCamp;
    int RandomOffset;
    bool isAccessible = true;
    TileVisualizer DisplayState;

    public GameObject Mesh;
    public int currentHeightStep = 0;
    public TilePos TilePos;
    public TileManager Manager;
    public  GameObject Child;
    public List<Tile> AdjacentTiles;

    public UnitEventHandler OnUnitTrespassing;
    public TileEventHandler OnDeactivate;
     
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsFree
    {
        get
        {
           
            return isAccessible && !isOccupied;
        }
    }
    bool isOccupied
    {
        get { return Child != null; }
    }

    public void Start()
    {

        if (WorldCrumbler.Instance != null)
        {
            WorldCrumbler.Instance.OnCrumble += OnCrumbleTurn;
            RandomOffset = Random.Range(0, WorldCrumbler.Instance.RandomCrumbleOffset + 1);
        }
        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
             b = gameObject.AddComponent<SelectibleObjectBase>();
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;
        b.OnSelect += delegate { TileSelecter.SelectTile(this); };
    }
    int turnsInCrumbleZone;
    int GetTurnsCrumbled()
    {
        return turnsInCrumbleZone - (CrumbleDelay + RandomOffset);
    }

    void UnitPassing(Unit u)
    {

    }
    void OnCrumbleTurn(int crumble_row)
    {
        if (crumble_row <= TilePos.z) return;
        // Debug.Log("cruble turn in tile " + crumble_row);
        turnsInCrumbleZone++;
        // Debug.Log(turnsInCrumbleZone);

        if (GetTurnsCrumbled() > 0)
        {
            SetVisualState("crumbling");
        }

        if ((GetTurnsCrumbled() > MinLifeTime && Random.value < WorldCrumbler.Instance.CrumbleChance)
            || GetTurnsCrumbled() > WorldCrumbler.Instance.CrumblingRows)
        {
            DeactivateTile();
        }

    }
    public void ToggleCamp()
    {
        isCamp = !isCamp;
        SetVisualState("normal");
    }
    public void OnHover()
    {
        
        TileSelecter.HoverTile(this);
    }

    void OnHoverEnd()
    {
      //  SetVisualState("normal");
    }

    void DeactivateTile()
    {
        if (OnDeactivate != null) OnDeactivate(this);
        WorldCrumbler.Instance.OnCrumble -= OnCrumbleTurn;
        GetComponent<MeshRenderer>().enabled = false;
        isAccessible = false;


    }

    public void SetChild(GameObject obj)
    {
        Child = obj;
    }

    public void SetMesh(GameObject newMesh)
    {
        if (Mesh != null)
        {
            DestroyImmediate(Mesh);
        }
        Mesh = newMesh;
        newMesh.transform.parent = this.transform;
        newMesh.transform.localPosition = Vector3.zero;

        newMesh.transform.position += Vector3.down * Mesh.transform.localScale.y / 2 * transform.localScale.y + Vector3.down * 0.1f;

    }

    public void ToggleBlocked()
    {

        if (!isOccupied)
        {

            isAccessible = !isAccessible;
        }
    }

    public void MoveTileUp()
    {
        currentHeightStep++;
        Elevate(Vector3.up * TileManager.HeighSteps);
    }

    void Elevate(Vector3 delta)
    {
        transform.position += delta;
        if (Child != null) Child.transform.position += delta;
    }

    public void MoveTileDown()
    {

        currentHeightStep--;
        Elevate(Vector3.up * TileManager.HeighSteps * -1);
    }


    void OnEnable()
    {
        DebugCrumbleTime = false;
    }

    public void SetVisualState(string id)
    {
        // Debug.Log("set visual");
        if (DisplayState == null) DisplayState = gameObject.GetComponent<TileVisualizer>();
        if (DisplayState == null) DisplayState = gameObject.AddComponent<TileVisualizer>();
        if (id == "normal" && isCamp) id = "camp";
        DisplayState.SetState(id);
    }

    #region 
    [HideInInspector]
    public bool DebugCrumbleTime = false;


    void OnDrawGizmos()
    {
        if (DebugCrumbleTime)
        {
            float p = (float)MinLifeTime / 10;
            Color col = new Color(2.0f * p, 2.0f * (1 - p), 0, 0.7f);
            Gizmos.color = col;

            Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x, 0.1f, transform.localScale.z));
        }

       Debug.DrawRay(transform.position, Vector3.up * 0.3f, IsFree ? Color.green : Color.red);

    }

    

    #endregion
}
