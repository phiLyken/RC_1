using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void TurnEvent(int turnId);

public class TurnSystem : MonoBehaviour {

    public  List<Player> players;
    Player CurrentPlayer;
    public static TurnSystem Instance;

    public TurnEvent OnTurnEnd;
    public TurnEvent OnTurnStart;

    public int currentTurn;


    public bool PlayerHasTurn(Player p)
    {
        return p == CurrentPlayer;
    }
    public int GetCurrentTurn()
    {
        return currentTurn;
    }
    public Player GetPlayer(int id)
    {
        foreach (Player p in players) if (p.PlayerID == id) return p;

        return null;
    }
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitForTurn());
    }
    IEnumerator WaitForTurn()
    {
        while (true)
        {
           
            if (OnTurnStart != null)
            {
                OnTurnStart(currentTurn);
            }

            foreach (Player p in players)
            {
                CurrentPlayer = p;          
                yield return StartCoroutine(p.ExecuteTurn());
            }


            currentTurn++;
       //     Debug.Log("turn ended");
            if (OnTurnEnd != null)
            {
                OnTurnEnd(currentTurn);
            }
        }
    }

 

    public void NextTurn()
    {
        //Debug.Log("Advance Turn");
        if (CurrentPlayer != null) CurrentPlayer.EndTurn();
    }
}
