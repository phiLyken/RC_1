using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public int PlayerID;
    public List<Unit> Units;
    public bool IsHuman;
       
    public IEnumerator ExecuteTurn()
    {
     
        while (!TurnEnded)
        {
            yield return null;
        }
        forceTurnEnd = false;
    }

    public void EndTurn()
    {
        forceTurnEnd = true;
    }
    private bool forceTurnEnd;
    bool TurnEnded { get { return forceTurnEnd || UnitsTurnEnded(); } }

    bool UnitsTurnEnded()
    {
        foreach(Unit u in Units)
        {
            if (!u.HasEndedTurn()) return false;
        }

        return true;
    }

    public void AddUnit(Unit u)
    {
        if (Units == null) Units = new List<Unit>();

        if (!Units.Contains(u)) Units.Add(u);
        
    }

    public void RemoveUnit(Unit u)
    {
        if (Units.Contains(u)) Units.Remove(u);
    }

    void Start()
    {
        foreach(Unit u in Unit.AllUnits)
        {
            if (u.OwnerID == PlayerID) AddUnit(u);
        }
    }
}
