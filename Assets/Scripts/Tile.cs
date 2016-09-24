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
    [SerializeField]
    public int TileGroup;

    [SerializeField]
    public int CrumbleStage;

    [SerializeField]
    public bool isCamp;

    [SerializeField]
    public bool isAccessible = true;

    [SerializeField]
    public bool isCrumbling;

    [SerializeField]
    public bool isEnabled = true;

    [SerializeField]
    public bool isBlockingSight;

    string MeshPath = "tile_mesh_states_ground_1";

    [SerializeField]
    public int currentHeightStep = 0;
    public TilePos TilePos;

    public GameObject Child;

    public UnitEventHandler OnUnitTrespassing;
    public TileEventHandler OnDeactivate;
    public TileEventHandler OnSetChild;
    public TileEventHandler OnTileCrumble;
    public TileEventHandler OnTileMove;
    public TileEventHandler OnTileRemoved;

    [HideInInspector]
    public bool customTile;

    public int GetCrumbleToHeightDiff()
    {
        return currentHeightStep + CrumbleStage;
    }
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

    public void InitGroup(int global_group)
    {
        TileGroup += global_group;
    }
    public void Start()
    {

        if (WorldCrumbler.Instance != null)
        {
            WorldCrumbler.Instance.OnCrumble += OnCrumbleTurn;            
        } else
        {
           // Debug.Log("Möp");
        }
        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
             b = gameObject.AddComponent<SelectibleObjectBase>();
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;
        b.OnSelect += delegate { TileSelecter.SelectTile(this); };
               
        SetBaseState();
    }

    public void SetBaseState()
    {
        MeshMaterialView view = GetComponent<MeshMaterialView>();
        view.states = new List<VisualState>();
       
        if (isCamp)
        {
            SetVisualState(new VisualState("base"));
        }
        else if (!isAccessible)
        {
            SetVisualState(new VisualState("disabled"));
        }
        else if (!isEnabled)
        {
            SetVisualState(new VisualState("disabled"));
        }
        else
        {
            SetVisualState(new VisualState("normal"));
        }
    }

    
    public void OnCrumbleTurn(int crumble_row)
    {
      
        if (!isCrumbling) return;

        int crumble_amount = (int) Constants.TileCrumbleRangeOnCrumble.Value();
       // Debug.Log("Crumble " + gameObject.name+"  amount"+crumble_amount);
        SetCrumble(CrumbleStage + crumble_amount);
    }

    public void SetCrumble(int new_crumble) {

        new_crumble = Mathf.Clamp(new_crumble,0, Constants.CrumbleHeightThreshold+1);
       
        CrumbleStage = new_crumble;

        if (OnTileCrumble != null) OnTileCrumble(this);
     
    }

    public void ToggleBlockSight()
    {
        customTile = true;
        isBlockingSight = !isBlockingSight;
    }
    public void ToggleCamp()
    {
        customTile = true;
        isCamp = !isCamp;

    }
    public void OnHover()
    {       
            TileSelecter.HoverTile(this);
    }

    void OnHoverEnd()
    {
        TileSelecter.UnhoverTile(this);
    }

    void DeactivateTile()
    {
       //Debug.Log("REMOVE " + gameObject.name);
      
        StartCoroutine(DeactivateWhenReady());
    }
    IEnumerator DeactivateWhenReady()
    {

        if (OnDeactivate != null) OnDeactivate(this);
        while (TurnEventQueue.Current != null) yield return null;
        RemoveTile();
    }


    void RemoveTile()
    {
       
        if(Application.isPlaying && WorldCrumbler.Instance != null)
            WorldCrumbler.Instance.OnCrumble -= OnCrumbleTurn;
        GetComponent<MeshRenderer>().enabled = false;
        isAccessible = false;
        isCrumbling = false;
        isEnabled = false;

        SetBaseState();
      //  Debug.Log("removed");

        if (OnTileRemoved != null) OnTileRemoved(this);

    }

    public void SetChild(GameObject obj)
    {
        Child = obj;
        if (OnSetChild != null) OnSetChild(this);
    }


    public void StartCrumble()
    {
        if (!isEnabled) return;

        isCrumbling = true;
      /*  if (crumble_effect == null)
        {
            crumble_effect = Instantiate(Resources.Load("crumble_prefab")) as GameObject;
            crumble_effect.transform.SetParent(transform);
            crumble_effect.transform.localPosition = Vector3.zero;
        }
        */

    }

    public void SetTileProperties(TileProperties p)
    {
        switch (p) { 
            case TileProperties.BlockWalkable:
                isAccessible = false;
            break;
        case TileProperties.BlockSight:
                isBlockingSight = true;
            break;
        default:
            break;
        }
    }
    public void ToggleBlocked()
    {
        if (!isOccupied)
        {
            customTile = true;
            isAccessible = !isAccessible;
        }
    }

    public void MoveTileUp(int steps)
    {
        customTile = true;
        currentHeightStep += steps;
        Elevate(Vector3.up * TileManager.HeighSteps * steps);
    }

    void Elevate(Vector3 delta)
    {
        transform.position += delta;
        if (OnTileMove != null) OnTileMove(this);
        if (Child != null) Child.transform.position += delta;
    }


    public void MoveTileDown(int steps)
    {
        customTile = true;
        currentHeightStep -= steps;

        Elevate(Vector3.down*TileManager.HeighSteps * steps);
        
        if ( Mathf.Abs(currentHeightStep) > Constants.CrumbleHeightThreshold)
        {
            DeactivateTile();
        }
    }

    public void ResetCrumble()
    {
        GetComponent<MeshRenderer>().enabled = true;
        isEnabled = true;
        isAccessible = true;
        isBlockingSight = false;

        currentHeightStep = 0;
       
        if (OnTileMove != null) OnTileMove(this);
        SetCrumble(0);
        SetBaseState();

        transform.localPosition = TileManager.Instance.GetTilePos(this);

    }

    public void SetVisualState(VisualState state)
    {
        MeshMaterialView view = GetComponent<MeshMaterialView>();      

        if (view == null)
        {
           
            view = gameObject.AddComponent<MeshMaterialView>();
        }
        
        view.AddState(state);
     
    }

    void OnDrawGizmos()
    {
       
        if (!isEnabled)
        {
            Gizmos.color = Color.red * 0.5f;
            Gizmos.DrawCube(GetPosition(), new Vector3(transform.localScale.x, 0.1f, transform.localScale.z));
        }if
        (!isAccessible)
        {
            Gizmos.color = Color.red * 0.8f;
            Gizmos.DrawWireCube(GetPosition(), new Vector3(transform.localScale.x * 0.8f, 0.1f, transform.localScale.z * 0.8f));

        }
        if (isCamp)
        {
            Gizmos.color = Color.green * 0.5f;
            Gizmos.DrawCube(GetPosition(), new Vector3(transform.localScale.x, 0.1f, transform.localScale.z));
          
        }
        if (isBlockingSight)
        {
            Gizmos.color = Color.yellow * 0.8f;
            Gizmos.DrawWireCube(GetPosition(), new Vector3( transform.localScale.x * 0.5f, 0.1f, transform.localScale.z * 0.5f));
        } 
        
    }


    public GameObject SpawnConfiguredMesh()
    {
        float start = Time.realtimeSinceStartup;

        GameObject prefab = Resources.Load("Tiles/Meshes/" + MeshPath) as GameObject;
        GameObject new_mesh = Instantiate(prefab);

       // Debug.Log("[SPAWN 1] " + (Time.realtimeSinceStartup - start).ToString("0.0000000000"));
        return SpawnMesh(new_mesh.GetComponent<TileMesh>());
       
    }

    public GameObject SpawnMesh(TileMesh child)
    {
        float start = Time.realtimeSinceStartup;
        child.SetTile(this);
        EditorTileMeshContainer.AddPair(this, child);
        //Debug.Log("[SPAWN 1.1] " + (Time.realtimeSinceStartup - start).ToString("0.0000000000"));
        return child.gameObject;


    }



}
