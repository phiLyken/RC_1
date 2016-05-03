using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public delegate void IntEventHandler(int turnId);
public delegate void TurnableEventHandler(ITurn turn);
public delegate void TurnListEventHandler(List<ITurn> turnables);

public class TurnSystem : MonoBehaviour {
    public Text TURNTF;
    public static TurnSystem Instance;
    public IntEventHandler OnGlobalTurn;
    public TurnListEventHandler OnListUpdated;

    public 
    bool forceNext;

    public ITurn Current;
    public static bool HasTurn(ITurn t)
    {
     //   Debug.Log("t " + t.GetID() + "  current" + Instance.Current.GetID());
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
    public List<ITurn> Turnables;

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
      
        if (OnGlobalTurn != null) OnGlobalTurn(t);
    }

    IEnumerator RunTurns()
    {
        //Skip first frame to give time to register
        yield return new WaitForSeconds(0.25f);
        SortListByTime();

        Current = GetNext();
        Debug.Log("Start Turn System " + Turnables.Count);

        while ( Current != null)
        {
            if (OnListUpdated != null) OnListUpdated(Turnables);
            Current.TurnTimeUpdated +=OnTurnPreview;

            Current.StartTurn();           

            yield return StartCoroutine(WaitForTurn(Current));

            Current.TurnTimeUpdated -= OnTurnPreview;
            Current.EndTurn();

            Current = null;
   
            currentTurn++;
                        
            GlobalTurn(currentTurn);

            yield return new WaitForSeconds(0.1f);
            NormalizeList();
            SortListByTime();

            Current = GetNext();

            forceNext = false;
            
        }
    }
	void OnTurnPreview(ITurn t){
		//Debug.Log("Resorting list for preview");
		SortListByTime();
	}
    ITurn GetNext()    {     
           
        return Turnables[0];
    }

    IEnumerator WaitForTurn(ITurn t)
    {

        ///Double null check because we are checking an interface
        while (!forceNext && (t!=null && !t.Equals(null)) && !t.HasEndedTurn())
        {
            //Debug.Log(t.GetID());
            yield return null;
        }       
    }

   /// <summary>
   /// Gets the lowest turn time from the current turnables
   /// </summary>
   /// <returns></returns>
    int getLowestTurnTime()
    {
        int lowest = 999;
        foreach (ITurn t in Turnables)
        {
            if (t.GetTurnTime() <= lowest)
            {
                lowest = t.GetTurnTime();
            }
        }

        return lowest;
    }

	/// <summary>
	/// places all turnables around 0 - the first one starting on 0
	/// </summary>
    void NormalizeList()
    {
        int lowest = Mathf.Max(0, getLowestTurnTime());
   //     Debug.Log("normalizing list lowest time " + lowest);
        foreach(ITurn t in Turnables)
        {
            t.SetNextTurnTime(-lowest);
        }
    }

    void SortListByTime()
    {
        if (Turnables == null) return;
	    Turnables = Turnables.OrderBy(o => o.GetTurnTime()+o.GetCurrentTurnCost()).ThenBy(o => o.StartOrderID).ToList();
        UpdateUnitListUI();

        if (OnListUpdated != null) OnListUpdated(Turnables);
    }

    void UpdateUnitListUI()
    {
        string s = "";
        foreach (string str in GetTurnList())
        {
           s+= str + "\n";
        }

		if(TURNTF != null)
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
        Instance.SortListByTime();
        return -1;
    }

    public static void Unregister(ITurn turnable)
    {
        if(turnable == Instance.Current)
        {
            Instance.Current = null;
        }
        if (Instance.Turnables.Contains(turnable))
        {
            Debug.Log("REMOVE " + turnable.GetID());
        
            Instance.Turnables.Remove(turnable);
        }
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
