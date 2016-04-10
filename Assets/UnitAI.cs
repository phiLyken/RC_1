using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UnitAI : MonoBehaviour, ITriggerable {
    public GameObject Cover;
    Unit m_unit;
    bool AttackingPlayer;

    void Awake()
    {
        AttackingPlayer = false;
        m_unit = GetComponent<Unit>();
        m_unit.OnTurnStart += StartTurn;
    }

    void StartTurn(Unit u)
    {
      //  Debug.Log(m_unit.GetID() + "...well....");
        StartCoroutine(AISequence());
    }


    IEnumerator Attack(Unit target)
    {
        Debug.Log("ai: attack");
        yield return null;
     //   Debug.Log(m_unit.GetID() + " Selecting atk");
        UnitAction_Attack Attack = m_unit.SelectAbility(2) as UnitAction_Attack;
        if (Attack  == null || !Attack.HasRequirements())
        {
            yield break;
        }
        yield return new WaitForSeconds(0.25f);
       
        target.OnHover();
        yield return new WaitForSeconds(0.25f);
        target.UnitSelected();
    }

    IEnumerator MoveRandom()
    {
        Debug.Log("ai: random");
        UnitAction_Move move = (m_unit.Actions[0] as UnitAction_Move);
        Tile randomDest = GetRandomWalkableTile(move);
        if(randomDest != null)
        {
            yield return StartCoroutine(Move(randomDest));
        } else
        {
            SkipTurn();
        }


    }
    IEnumerator Move(Tile t)
    {
        Debug.Log("ai: move");
        yield return null;
        // Debug.Log(m_unit.GetID()+ "Selecting move");
        UnitAction_Move move = (UnitAction_Move)m_unit.SelectAbility(0);
       
        if (move == null || !move.HasRequirements()) yield break;

        yield return new WaitForSeconds(0.5f);
        //  Debug.Log(m_unit.GetID() + " getting move targets");

        t.OnHover();
        yield return new WaitForSeconds(0.5f);

        // Debug.Log(m_unit.GetID() + " Select tile to for move "+best.TilePos);
        TileSelecter.SelectTile(t);
    }
    IEnumerator MoveToAttackPosition()
    {
        Debug.Log("ai: move to attack position");
        UnitAction_Attack atk = (m_unit.Actions[2] as UnitAction_Attack);
        UnitAction_Move  move = (m_unit.Actions[0] as UnitAction_Move);
        List<Tile> walkables = move.GetWalkableTiles(m_unit.currentTile);
        List<Unit> all_enemies = Unit.GetAllUnitsOfOwner(0);

        foreach(Tile t in walkables)
        {
            foreach(Unit u in all_enemies)
            {
                if (UnitAction_Attack.isInRange(m_unit, u, atk.Range, t)){
                    yield return StartCoroutine(Move(t));
                    yield break;
                }
            }
        }

        yield return StartCoroutine( MoveRandom());

    }
    List<Tile> GetWalkableTiles(UnitAction_Move m)
    {
        return m.GetWalkableTiles(m_unit.currentTile); 
    }
    Tile GetRandomWalkableTile(UnitAction_Move m)
    {
        return FindBestWalkableTile(GetWalkableTiles(m) );
    }
    IEnumerator AISequence()
    {
        while( !(m_unit as ITurn).HasEndedTurn()){

            yield return StartCoroutine( Decide());
        }

    }
    IEnumerator Decide()
    {

        if (AttackingPlayer)
        {
       // Debug.Log(m_unit.GetID() + "decide");
        List<Unit> attackables = UnitAction_Attack.GetAttackableUnits(Unit.GetAllUnitsOfOwner(0), m_unit, (m_unit.Actions[2] as UnitAction_Attack).Range);
        Unit target = FindBestUnitToAttack(attackables);

            if (target != null)
            {
                yield return StartCoroutine(Attack(target));
            }
            else
            {
                yield return StartCoroutine( MoveToAttackPosition());
            }
        } else
        {
            yield return StartCoroutine(MoveRandom());
        }

       
    }
    void SkipTurn()
    {
        Debug.Log("ai skip turn");
        m_unit.SkipTurn();
    }
    Tile GetAttackTile()
    {
        return null;
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

    public void OnTrigger()
    {
        Cover.SetActive(false);
        Debug.Log(m_unit.GetID() + " now attacking");
        AttackingPlayer = true;
    }
}
