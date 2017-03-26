using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;



public class TurnSystem : MonoBehaviour {
    public Text DebugOutPut;
    static TurnSystem _instance;
    public static TurnSystem Instance
    {
        get { return _instance == null ? GetInstance() : _instance; }
    }

    public IntEventHandler OnGlobalTurn;
    public Action<List<ITurn>> OnListUpdated;
    public Action<ITurn> OnStartTurn;

    public bool InitOnStart;

    int currentTurn;
    public List<ITurn> Turnables;

    bool forceNext;

    public ITurn Current;
    public static bool HasTurn(ITurn t)
    {
     //   MDebug.Log("t " + t.GetID() + "  current" + Instance.Current.GetID());
        if (Instance == null || Instance.Current == null || Instance.Current != t) return false;
        return true;
    }

    static TurnSystem GetInstance()
    {
        _instance = GameObject.FindObjectOfType<TurnSystem>();
        return _instance;
    }
    public int GetCurrentTurn()
    {
        return currentTurn;
    }

    public void NextTurn()
    {
        Current.SkipTurn(); 
    }


    void Awake()
    {
        _instance = this;
        TurnEventQueue.Reset();
    }
    void Start()
    {
        if (InitOnStart)
            Init();
    }
    public void Init()
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
        yield return new WaitForSeconds(0.1f);
        SortListByTime();

        Current = GetNext();
        
        while ( Current != null && !GameEndListener.GameEnded )
        {
            OnListUpdated.AttemptCall(Turnables);
            Current.TurnTimeUpdated +=OnTurnPreview;
            Current.StartTurn();
            OnStartTurn.AttemptCall(Current);            

            yield return StartCoroutine(WaitForTurn(Current));

            if(Current != null && !Current.Equals(null)) { 
                Current.TurnTimeUpdated -= OnTurnPreview;
                Current.EndTurn();
            }

            Current = null;
   
            currentTurn++;
                        
            GlobalTurn(currentTurn);

            yield return new WaitForSeconds(0.5f);
            NormalizeList();
            SortListByTime();

            Current = GetNext();

            forceNext = false;
 
        }
    }
	void OnTurnPreview(ITurn t){
		//MDebug.Log("Resorting list for preview");
		SortListByTime();
	}
    ITurn GetNext()    {     
           
        return Turnables[0];
    }

    IEnumerator WaitForTurn(ITurn t)
    {

        ///Double null check because we are checking an interface
        while (!forceNext && ( (t!=null && !t.Equals(null)) && !t.HasEndedTurn() || TurnEventQueue.EventRunning  ))
        {

           // MDebug.Log("^turnSystem " + t.GetID() + " Events:" + TurnEventQueue.EventRunning.ToString() + "  hasEnded" + t.HasEndedTurn() + "\n" + TurnEventQueue.ToString2());
            yield return null;
        }       
    }

   /// <summary>
   /// Gets the lowest turn time from the current turnables
   /// </summary>
   /// <returns></returns>
    public float GetLowestTurnTime()
    {
        float lowest = 999;
        foreach (ITurn t in Turnables)
        {
            if (t.GetTurnTime() <= lowest)
            {
                lowest = t.GetTurnTime();
            }
        }
      //  MDebug.Log("lowest " + lowest);
        return lowest;
    }

	/// <summary>
	/// places all turnables around 0 - the first one starting on 0
	/// </summary>
    void NormalizeList()
    {
        float lowest = GetLowestTurnTime();
       //  MDebug.Log("normalizing list lowest time " + lowest);
        foreach(ITurn t in Turnables)
        {
            t.SetNextTurnTime(-lowest);
        }
    }

    void SortListByTime()
    {
        if (Turnables == null) return;
	    Turnables = Turnables.OrderBy(o => o.GetTurnTime()+o.GetCurrentTurnCost()).ThenBy(o => o.StartOrderID).ToList();
       // UpdateUnitListUI();

       OnListUpdated.AttemptCall(Turnables);
    }

    void UpdateUnitListUI()
    {
        string s = "";
        foreach (string str in GetTurnList())
        {
           s+= str + "\n";
        }

		if(DebugOutPut != null)
        DebugOutPut.text = s;

    }

    public static int Register(ITurn new_turnable)
    {
        if (Instance == null)
            return 0;

        if (Instance.Turnables == null)
        {
            Instance.Turnables = new List<ITurn>();
        }

        if (!Instance.Turnables.Contains(new_turnable))
        {
            MDebug.Log("^turnSystem TURNABLE: register " + new_turnable.GetID());
            Instance.Turnables.Add(new_turnable);
           // Instance.NormalizeList();
            Instance.SortListByTime();        
            Instance.OnListUpdated.AttemptCall(Instance.Turnables);

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
            MDebug.Log("^turnSystem REMOVE " + turnable.GetID());        
            Instance.Turnables.Remove(turnable);
            Instance.OnListUpdated.AttemptCall(Instance.Turnables);
         
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
