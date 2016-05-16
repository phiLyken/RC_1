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

    [SerializeField]
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
        } else
        {
            Debug.Log("Möp");
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

        int crumble_amount = (int)Constants.CrumbleRange.Value();
      //  Debug.Log("Crumble " + gameObject.name+"  amount"+crumble_amount);
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
        Debug.Log("REMOVE " + gameObject.name);
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

        RemoveCrumbleEffect();

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

    void RemoveCrumbleEffect()
    {
        if (crumble_effect != null)
        {
            DestroyImmediate(crumble_effect.gameObject);
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

        if (currentHeightStep < Constants.CrumbleHeightThreshold)
        {
            DeactivateTile();
        }
    }

    public void ResetCrumble()
    {
        CrumbleStage = 0;
        currentHeightStep = 0;
        transform.position = TileManager.Instance.GetTilePos(this);
        GetComponent<MeshRenderer>().enabled = true;
        isAccessible = true;
        RemoveCrumbleEffect();
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
