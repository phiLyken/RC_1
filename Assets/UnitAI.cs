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


    IEnumerator Attack(Unit target)
    {
       
        yield return null;
        Debug.Log("Selecting atk");
        m_unit.SelectAbility(2);
        yield return new WaitForSeconds(0.5f);
       
        target.OnHover();
        yield return new WaitForSeconds(0.5f);
        target.UnitSelected();
    }

    IEnumerator Move()
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
    IEnumerator AISequence()
    {
        while( !(m_unit as ITurn).HasEndedTurn()){
            yield return StartCoroutine( Decide());
        }

    }
    IEnumerator Decide()
    {
        List<Unit> attackables = UnitAction_Attack.GetAttackableUnits(Unit.GetAllUnitsOfOwner(0), m_unit, (m_unit.Actions[2] as UnitAction_Attack).Range);
        Unit target = FindBestUnitToAttack(attackables);

        if(target != null)        {
            yield return StartCoroutine(Attack(target));
        } else
        {
            yield return StartCoroutine(Move());
        }
    }
    Tile FindBestWalkableTile(List<Tile> tiles)
    {
        return tiles[Random.Range(0, tiles.Count)];
    }

    Unit FindBestUnitToAttack(List<Unit> units)
    {
        if (units.Count == 0) return null;

        return units[Random.Range(0, units.Count)];
    }
}
