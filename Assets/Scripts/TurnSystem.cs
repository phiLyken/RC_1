using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void TurnEvent(int turnId);

public class TurnSystem : MonoBehaviour {

    public static TurnSystem Instance;
    bool forceNext;

    public int GetCurrentTurn()
    {
        return currentTurn;
    }

    public void NextTurn()
    {
        forceNext = true;
    }
    int currentTurn;
    List<ITurn> Turnables;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(RunTurns());
    }

    IEnumerator RunTurns()
    {
        ITurn next = GetNext();
        Debug.Log("Start Turn System " + Turnables.Count);
        while ( next != null)
        {
            Debug.Log("turn " + currentTurn);
            next.StartTurn();

            yield return StartCoroutine(WaitForTurn(next));
            
            currentTurn++;

            foreach (ITurn t in Turnables) t.GlobalTurn();

            next.SetNextTurnTime(next.GetTurnTimeCost());

            SortListByTime();

            next = GetNext();

            forceNext = false;
        }
    }

    ITurn GetNext()    {        
        return Turnables[0];
    }

    IEnumerator WaitForTurn(ITurn t)
    {
     
        while (!forceNext && t != null && !t.HasEndedTurn() ) yield return null;
       
    }

    void SortListByTime()
    {
       Turnables = Turnables.OrderBy(o => o.GetNextTurnTime()).ToList();
        foreach (string str in GetTurnList()) Debug.Log(str + "\n");
    }


    public static bool Register(ITurn new_turnable)
    {
        if (Instance.Turnables == null)
        {
            Instance.Turnables = new List<ITurn>();
        }

        if (!Instance.Turnables.Contains(new_turnable))
        {
            Debug.Log("TURNABLE: register " + new_turnable.GetID());
            Instance.Turnables.Add(new_turnable);
            return true;
        }

        return false;
    }

    public static void Unregister(ITurn turnable)
    {
        if (Instance.Turnables.Contains(turnable)) Instance.Turnables.Remove(turnable);
    }

    public string[] GetTurnList()
    {
        string[] ids = new string[Turnables.Count];
        for(int i = 0; i < ids.Length; i++)
        {
            ids[i] = (i+1).ToString()+"-"+Turnables[i].GetID();
        }

        return ids;
    }
}
