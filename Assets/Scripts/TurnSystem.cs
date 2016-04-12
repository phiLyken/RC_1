using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public delegate void TurnEvent(int turnId);
public delegate void TurnableEvent(ITurn turn);

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
        Current.SkipTurn(); 
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
        foreach (ITurn turnable in Turnables) turnable.GlobalTurn(t);
        if (OnGlobalTurn != null) OnGlobalTurn(t);
    }
    IEnumerator RunTurns()
    {
        yield return null;
        SortListByTime();
        ITurn next = GetNext();
        Debug.Log("Start Turn System " + Turnables.Count);
        while ( next != null)
        {
			next.OnTurnPreview +=OnTurnPreview;
            Debug.Log("Turn  no#" + currentTurn);
            next.StartTurn(); 
            Current = next;

            yield return StartCoroutine(WaitForTurn(next));

			next.OnTurnPreview -= OnTurnPreview;
            next.SetNextTurnTime(next.GetTurnTimeCost());
            currentTurn++;
            GlobalTurn(currentTurn);
            

            NormalizeList();
            SortListByTime();


            next = GetNext();

            forceNext = false;
        }
    }
	void OnTurnPreview(ITurn t){
		Debug.Log("Resorting list for preview");
		SortListByTime();
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
    int getLowestTurnTime()
    {
        int lowest = 999;
        foreach (ITurn t in Turnables)
        {
            if (t.GetNextTurnTime() <= lowest)
            {
                lowest = t.GetNextTurnTime();
            }
        }

        return lowest;
    }

	/// <summary>
	/// places all turnables around 0 - the first one starting on 0
	/// </summary>
    void NormalizeList()
    {
        int lowest = getLowestTurnTime();
        Debug.Log("normalizing list lowest time " + lowest);
        foreach(ITurn t in Turnables)
        {
            t.SetNextTurnTime(t.GetNextTurnTime() - lowest);
        }
    }

    void SortListByTime()
    {
		Turnables = Turnables.OrderBy(o => o.GetNextTurnTime()).ThenBy(o => o.StartOrderID).ToList();
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
    public static int Register(ITurn new_turnable)
    {
        if (Instance.Turnables == null)
        {
            Instance.Turnables = new List<ITurn>();
        }

        if (!Instance.Turnables.Contains(new_turnable))
        {
            Debug.Log("TURNABLE: register " + new_turnable.GetID());
            Instance.Turnables.Add(new_turnable);
			return Instance.Turnables.Count;
        }

        return -1;
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
