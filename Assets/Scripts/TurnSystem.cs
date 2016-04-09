using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public delegate void TurnEvent(int turnId);


public class TurnSystem : MonoBehaviour {
    public Text TURNTF;
    public static TurnSystem Instance;
    public TurnEvent OnGlobalTurn;

    bool forceNext;

    public ITurn Current;
    public static bool HasTurn(ITurn t)
    {
        if (Instance.Current == null || Instance.Current != t) return false;
        return true;
    }
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

    public void GlobalTurn(int t)
    {
        foreach (ITurn turnable in Turnables) turnable.GlobalTurn();
        if (OnGlobalTurn != null) OnGlobalTurn(t);
    }
    IEnumerator RunTurns()
    {
        SortListByTime();
        ITurn next = GetNext();
        Debug.Log("Start Turn System " + Turnables.Count);
        while ( next != null)
        {
            Debug.Log("Turn  no#" + currentTurn);
            next.StartTurn();
            Current = next;
            yield return StartCoroutine(WaitForTurn(next));
            
            currentTurn++;

            GlobalTurn(currentTurn);
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
        while (!forceNext && (t!=null && !t.Equals(null)) && !t.HasEndedTurn())
        {
            //Debug.Log(t.GetID());
            yield return null;
        }       
    }

    
    void SortListByTime()
    {
       Turnables = Turnables.OrderBy(o => o.GetNextTurnTime()).ToList();
       UpdateUnitListUI();
    }

    void UpdateUnitListUI()
    {
        string s = "";
        foreach (string str in GetTurnList())
        {
           s+= str + "\n";
        }
        TURNTF.text = s;

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
