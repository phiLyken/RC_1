using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAI : MonoBehaviour {

    Unit m_unit;

    void Awake()
    {
        m_unit = GetComponent<Unit>();
        m_unit.OnTurnStart += StartTurn;
    }

    void StartTurn(Unit u)
    {
        Debug.Log("...well....");
        StartCoroutine(AISequence());
    }

    IEnumerator AISequence()
    {
        yield return null;
        Debug.Log("Selecting move");
        m_unit.SelectAbility(0);
        yield return new WaitForSeconds(1);
        Debug.Log("Getting targets");
        List<Tile> walkable = (m_unit.Actions[0] as UnitAction_Move).GetWalkableTiles(m_unit.currentTile);
        Tile best = FindBestWalkableTile(walkable);
        best.OnHover();
        yield return new WaitForSeconds(1);
        Debug.Log("Select tile");
        TileSelecter.SelectTile(best);
               
        
    }

    Tile FindBestWalkableTile(List<Tile> tiles)
    {
        return tiles[Random.Range(0, tiles.Count)];
    }
}
