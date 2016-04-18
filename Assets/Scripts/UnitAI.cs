using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UnitAI : MonoBehaviour, ITriggerable {
    public GameObject Cover;
    Unit m_unit;
    ActionManager m_Actions;
    bool AttackingPlayer;

    void Awake()
    {
        AttackingPlayer = false;
        m_unit = GetComponent<Unit>();
        m_Actions = GetComponent<ActionManager>();
        m_unit.OnTurnStart += StartTurn;

        m_unit.OnDamageReceived += dmg =>
        {
            OnTrigger();
        };
    }

    void StartTurn(Unit u)
    {
        Debug.Log(m_unit.GetID() + "...well....");
        StartCoroutine(AISequence());
    }

    UnitAction_Attack getAttack()
    {
        UnitAction_Attack atk = (m_unit.Actions.GetAction("Attack") as UnitAction_Attack); 
        if(atk == null)
        {
            Debug.LogWarning(" could not find Attack ability for " + m_unit.GetID());
        }
        return atk;
    }

    UnitAction_Move getMove()
    {

        UnitAction_Move move = (m_unit.Actions.GetAction("Move") as UnitAction_Move);
        if (move == null)
        {
            Debug.LogWarning(" could not find Move ability for " + m_unit.GetID());
        }
        return move;
    }
    IEnumerator Attack(Unit target)
    {
        Debug.Log("ai: attack");
       
        yield return null;
        //   Debug.Log(m_unit.GetID() + " Selecting atk");
        UnitAction_Attack Attack = getAttack();
        Debug.Log(target);
        if (Attack  == null || !Attack.HasRequirements())
        {
          
            yield break;
        }
       
        m_Actions.SelectAbility(Attack);
        yield return new WaitForSeconds(0.25f);

        target.OnHover();
        yield return new WaitForSeconds(0.25f);
        target.UnitSelected();
    }

    IEnumerator MoveRandom()
    {
        Debug.Log("ai: random");
        UnitAction_Move move = getMove();
        move.SelectAction();
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
        Debug.Log(m_unit.GetID()+ "Selecting move");
        UnitAction_Move move = getMove();
        m_Actions.SelectAbility(move);

        if (move == null || !move.HasRequirements()) yield break;

        yield return new WaitForSeconds(0.5f);
        Debug.Log(m_unit.GetID() + " getting move targets");

        t.OnHover();
        yield return new WaitForSeconds(0.5f);

        Debug.Log(m_unit.GetID() + " Select tile to for move ");
        TileSelecter.SelectTile(t);
        
    }
    IEnumerator MoveToAttackPosition()
    {
        Debug.Log("ai: move to attack position");
        UnitAction_Attack atk = getAttack();
        UnitAction_Move  move = getMove();

        List<Tile> walkables = move.GetWalkableTiles(m_unit.currentTile);
        List<Unit> all_enemies = Unit.GetAllUnitsOfOwner(0, false);

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
        ///We need to wait a frame because the Action system needs to reset first, and both listen to "onstart" of the unit
        yield return null;
        while ( !(m_unit).HasEndedTurn()){
            Debug.Log("13123");
            yield return StartCoroutine( Decide());
        }
        Debug.Log("Ai ended");

    }
    IEnumerator Decide()
    {
        if (m_Actions.IsActionInProgress) yield return null;

        if (AttackingPlayer)
        {
             Debug.Log(m_unit.GetID() + "decide");
             List<Unit> attackables = UnitAction_Attack.GetAttackableUnits(Unit.GetAllUnitsOfOwner(0, false), m_unit, getAttack().Range);
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
        m_unit.Activate();
        Cover.SetActive(false);
        Debug.Log(m_unit.GetID() + " now attacking");
        AttackingPlayer = true;
    }
}
