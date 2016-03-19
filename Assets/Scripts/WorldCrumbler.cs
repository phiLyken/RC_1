using UnityEngine;
using System.Collections;

public delegate void CrumbleEvent(int row);
public class WorldCrumbler : MonoBehaviour {

    //Speed: Rows per turn the crumble progresses
    //Every Row < than the crumble row will get 1 crumbleturn
    public int CrumbleSpeed;
    public int CrumblingRows;
    public int RandomCrumbleOffset;
    public float CrumbleChance;
    public static  WorldCrumbler Instance;
    public CrumbleEvent OnCrumble;
    int currentTurn;
    float currentCrumbleLine;
    int currentCrumbleRow
    {
        get { return (int)currentCrumbleLine; }
    }

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if(TurnSystem.Instance != null)
        {
            TurnSystem.Instance.OnTurnEnd += Turn;
            currentTurn = TurnSystem.Instance.GetCurrentTurn();
        }
        
    }

    void Turn(int turn)
    {
        currentCrumbleLine += CrumbleSpeed;
        if(OnCrumble != null)
        {
            OnCrumble(currentCrumbleRow);
        }
    }

    void OnDrawGizmos()
    {
        if(TileManager.Instance != null)
        {
            Gizmos.color = new Color(0.5f, 0, 0, 0.5f);
            Gizmos.DrawCube(
                TileManager.Instance.GetTilePos2D(TileManager.Instance.GridWidth / 2, currentCrumbleRow),
                new Vector3(TileManager.Instance.GridWidth,1,1 )
                );
        }
    }
}
