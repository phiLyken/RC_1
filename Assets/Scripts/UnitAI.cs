using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class UnitAI : MonoBehaviour, ITriggerable {

    public int group_id;
    public GameObject Cover;
    Unit m_unit;
    ActionManager m_Actions;
    bool Triggered;

    void Awake()
    {
        Triggered = false;
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

     
        StartCoroutine(AISequence());
    }

    UnitAction_ApplyEffectFromWeapon getAttack()
    {
        //TODO: Remove reference by string for actions
        return m_Actions.GetAcionOfType<UnitAction_ApplyEffectFromWeapon>();
    }

    UnitAction_Move getMove()
    {
        return m_Actions.GetAcionOfType<UnitAction_Move>();
    }

    IEnumerator Attack(Unit target)
    {
       // Debug.Log("ai: attack");
       
        yield return null;
        //   Debug.Log(m_unit.GetID() + " Selecting atk");
        UnitAction_ApplyEffectFromWeapon Attack = getAttack();
        Debug.Log(target);
        if (Attack  == null || !Attack.HasRequirements(true))
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
       // Debug.Log("ai: random");
        UnitAction_Move move = getMove();
      
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
      //  Debug.Log(m_unit.GetID()+ "Selecting move");
        UnitAction_Move move = getMove();
        m_Actions.SelectAbility(move);

        if (move == null || !move.HasRequirements(true)) yield break;

        yield return new WaitForSeconds(0.5f);
        //Debug.Log(m_unit.GetID() + " getting move targets");

        t.OnHover();
        yield return new WaitForSeconds(0.5f);

       // Debug.Log(m_unit.GetID() + " Select tile to for move ");
        TileSelecter.SelectTile(t);
        
    }
    IEnumerator MoveToAttackPosition()
    {
        //  Debug.Log("ai: move to attack position");
        UnitAction_ApplyEffectFromWeapon atk = getAttack();
        UnitAction_Move  move = getMove();

        List<Tile> walkables = move.GetWalkableTiles(m_unit.currentTile);
        List<Unit> all_enemies = Unit.GetAllUnitsOfOwner(0, false);

        foreach(Tile t in walkables)
        {
            foreach(Unit u in all_enemies)
            {
                if (UnitAction_ApplyEffect.isInRange(m_unit, u, atk.GetRange() )){
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
            
            yield return StartCoroutine( Decide());
        }
       // Debug.Log("Ai ended");

    }
    IEnumerator Decide()
    {
        if (m_Actions.IsActionInProgress) yield return null;

        if (Triggered)
        {
            //  Debug.Log(m_unit.GetID() + "decide");
            List<Unit> attackables = getAttack().GetTargetableUnits(Unit.AllUnits);

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
       // Debug.Log("ai skip turn");
        m_unit.SkipTurn();
    }
    Tile GetAttackTile()
    {
        return null;
    }
    Tile FindBestWalkableTile(List<Tile> tiles)
    {
		if(tiles.Count == 0) return null;
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
      //  Debug.Log(m_unit.GetID() + " now attacking");
        Triggered = true;

        TriggerUnitsForGroup(this);
    }

    public static void TriggerUnitsForGroup(UnitAI unit)
    {
        int id = unit.group_id;

        Unit.AllUnits.ForEach(u =>
        {
            if (
                 u.OwnerID == 1 &&
                 !u.gameObject.GetComponent<UnitAI>().Triggered &&
                 u.gameObject.GetComponent<UnitAI>().group_id == id &&
                 u != unit
            )
            {
                u.GetComponent<UnitAI>().OnTrigger();
            }
            }
        );
    }
}
