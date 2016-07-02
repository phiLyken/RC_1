using UnityEngine;
using System.Collections;

public class CrumbleEdgeBorderManager : MonoBehaviour {

    public GameObject BorderPrefab;

    public CrumbleEdgeBorder left;
    public CrumbleEdgeBorder right;

    public void SpawnEdges()
    {
        if(BorderPrefab == null)
        {
            Debug.LogWarning("No Border Prefab Found");
            return;
        }
        if(left != null)
        {
            left.Remove();
        }

        if(right != null)
        {
            right.Remove();
        }

        left =  new CrumbleEdgeBorder(2, -1, 0, BorderPrefab);
        right = new CrumbleEdgeBorder(3, 1, TileManager.Instance.GridWidth-1, BorderPrefab);    
    } 

    void Start()
    {
        SpawnEdges();
    }

    [System.Serializable]
    public class CrumbleEdgeBorder
    {
        int m_Corner;
        int m_Scale;
        int m_Col;
        
        [SerializeField]        
        public   GameObject m_BorderGO;
        Tile m_CurrentTile;

        public CrumbleEdgeBorder(int corner, int scale_direction, int target_column, GameObject prefab)
        {
            m_Corner = corner;
            m_Scale = scale_direction;
            m_Col = target_column;
            m_BorderGO = Instantiate(prefab) as GameObject;
            m_BorderGO.transform.localScale = new Vector3(m_BorderGO.transform.localScale.x * m_Scale, m_BorderGO.transform.localScale.y, m_BorderGO.transform.localScale.z);
            m_BorderGO.transform.localRotation = Quaternion.Euler(270, 0, 0);
            UpdateToNewTile(GetNextTile());
        }

        void OnMyTileUpdate(Tile t)
        {
            if(t.CrumbleStage != 0)
            {
                t.OnTileCrumble -= OnMyTileUpdate;
            }
            UpdateToNewTile(GetNextTile());
        }

        void UpdateToNewTile(Tile t)
        {
            if (t == null) return;

            m_CurrentTile = t;
            m_CurrentTile.OnTileCrumble += OnMyTileUpdate;

            m_BorderGO.transform.position = MyMath.GetTransformBoundPositionTop(t.transform)[m_Corner];
        }

        Tile GetNextTile()
        {
           return TileManager.Instance.GetFirstNotCrumblingTileInCol(m_Col);
        }

        public void Remove()
        {
            if(m_CurrentTile != null)
                m_CurrentTile.OnTileCrumble -= OnMyTileUpdate;

            DestroyImmediate(m_BorderGO);
        }
    }
}
