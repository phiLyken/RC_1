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

    [SerializeField]
    public int CrumbleStage;
    GameObject crumble_effect;

    [SerializeField]
    public bool isCamp;

    [SerializeField]
    public bool isAccessible = true;

    [SerializeField]
    public bool isCrumbling;

    public GameObject Mesh;
    public int currentHeightStep = 0;
    public TilePos TilePos;
    public TileManager Manager;
    public GameObject Child;
    public List<Tile> AdjacentTiles;

    public UnitEventHandler OnUnitTrespassing;
    public TileEventHandler OnDeactivate;
    public TileEventHandler OnSetChild;
    
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
            
        }
        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
             b = gameObject.AddComponent<SelectibleObjectBase>();
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;
        b.OnSelect += delegate { TileSelecter.SelectTile(this); };

        SetBaseState();
    }

    void SetBaseState()
    {
        if (isCamp) {
            SetVisualState( new VisualState("base"));
        } else if (!isAccessible)
        {
            SetVisualState(new VisualState("blocked"));
        } else
        {
            SetVisualState(new VisualState("normal"));
        }
    }
    public void OnCrumbleTurn(int crumble_row)
    {
        if (!isCrumbling) return;

        int crumble_amount = Random.Range(0, 3);
        SetCrumble(CrumbleStage + crumble_amount);
    }

    public void SetCrumble(int new_crumble) {

        if (!isAccessible) return;
        int diff = new_crumble - CrumbleStage;
        CrumbleStage = new_crumble;
        MoveTileDown( Mathf.Max(diff,0));
    }

    public void ToggleCamp()
    {
        isCamp = !isCamp;

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

        if(Application.isPlaying)
            WorldCrumbler.Instance.OnCrumble -= OnCrumbleTurn;
        GetComponent<MeshRenderer>().enabled = false;
        isAccessible = false;

        if(crumble_effect != null)
        {
            DestroyImmediate(crumble_effect.gameObject);
        }

    }

    public void SetChild(GameObject obj)
    {
        Child = obj;
        if (OnSetChild != null) OnSetChild(this);
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

    public void StartCrumble()
    {
        isCrumbling = true;
        if (isAccessible && crumble_effect == null)
        {
            crumble_effect = Instantiate(Resources.Load("crumble_prefab")) as GameObject;
            crumble_effect.transform.SetParent(transform);
            crumble_effect.transform.localPosition = Vector3.zero;
        }

    }

    public void ToggleBlocked()
    {
        if (!isOccupied)
        {
            isAccessible = !isAccessible;
        }
    }

    public void MoveTileUp(int steps)
    {
        currentHeightStep += steps;
        Elevate(Vector3.up * TileManager.HeighSteps * steps);
    }

    void Elevate(Vector3 delta)
    {
        transform.position += delta;
        if (Child != null) Child.transform.position += delta;
    }

    public void MoveTileDown(int steps)
    {

        currentHeightStep -= steps;
        Elevate(Vector3.up * TileManager.HeighSteps * -steps);

        if (currentHeightStep < -4)
        {
            DeactivateTile();
        }
    }


    void OnEnable()
    {
        DebugCrumbleTime = false;
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

    #region 
    [HideInInspector]
    public bool DebugCrumbleTime = false;


    void OnDrawGizmos()
    {
        if (DebugCrumbleTime)
        {
            float p = (float) CrumbleStage / 10;
            Color col = new Color(2.0f * p, 2.0f * (1 - p), 0, 0.7f);
            Gizmos.color = col;

            Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x, 0.1f, transform.localScale.z));
        }

     //  Debug.DrawRay(transform.position, Vector3.up * 0.3f, IsFree ? Color.green : Color.red);
    
       
    }

    

    #endregion
}
