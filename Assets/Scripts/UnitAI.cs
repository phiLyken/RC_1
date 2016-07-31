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
    Unit preferred_target;

    Tile startTile;

    Tile GetPatrolTile() {
        return Triggered ? m_unit.currentTile : startTile;
    }

    void Awake()
    {
        Triggered = false;
        m_unit = GetComponent<Unit>();
        m_Actions = GetComponent<ActionManager>();
        m_unit.OnTurnStart += StartTurn;
        startTile = m_unit.currentTile;

        m_unit.OnDamageReceived += dmg =>
        {
            OnTrigger(dmg.Instigator as Unit);
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

    void OnDmgReceived(UnitEffect_Damage dmg)
    {
        if (!Triggered)
        {
            OnTrigger(dmg.Instigator as Unit);
        } else
        {
            if(Random.value < 0.15f)
            {
                preferred_target = dmg.Instigator as Unit;
            }
        }
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
       // Debug.Log(target);
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
    IEnumerator MovePatrol()
    {
        Tile patrolDest = GetPatrolTile(m_unit.currentTile);
        if(patrolDest != null)
        {
            yield return StartCoroutine( Move(patrolDest)) ;
        } else
        {
            SkipTurn();
            yield break;
        }
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

        Debug.Log(m_unit.GetID() + " Select tile to for move ");
        TileSelecter.SelectTile(t);
        yield return new WaitForSeconds(0.15f);
        
    }

     Dictionary<Unit, List<Tile>> GetEnemiesAttackableWithin1Move()
    {
        UnitAction_ApplyEffectFromWeapon atk = getAttack();
        UnitAction_Move move = getMove();

        List<Tile> walkable_tiles = move.GetWalkableTiles(m_unit.currentTile);
      //  Debug.Log("walkable tiles " + walkable_tiles.Count);

        List<Unit> enemy_group = Unit.GetAllUnitsOfOwner(0, false);

        Dictionary<Unit, List<Tile>> unit_map = new Dictionary<Unit, List<Tile>>();

        foreach (Tile t in walkable_tiles)
        {
            foreach (Unit u in enemy_group)
            {
                
                if (atk.CanTarget(u, t))
                {

                    if (!unit_map.ContainsKey(u))
                    {
                      unit_map.Add(u, new List<Tile>());                       
                    }
                    unit_map[u].Add(t);
                }
            }
        }


        return unit_map;
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
                if (UnitAction_ApplyEffect.IsInRangeAndHasLOS(m_unit, u, atk.GetRange() )){
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

    Tile GetPatrolTile(Tile start)
    {
        List<Tile> tiles_in_range = TileManager.Instance.GetRandomTilesAroundCenter(start, Constants.AI_PATROL_DISTANCE);
        Debug.Log("PATROL query tiles in range = " + tiles_in_range.Count);
        tiles_in_range.RemoveAll(t => !t.isAccessible && !t.isEnabled && t.isCamp && t.isCrumbling);

        return MyMath.GetRandomObject(tiles_in_range);
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

    Unit FindPreferredTarget()
    {
    return     MyMath.GetRandomObject(Unit.GetAllUnitsOfOwner(0, true));
    }

    Tile GetBestAttackPosition(Unit target, List<Tile> from_tiles)
    {
        List<GenericWeightable<Tile>> weighted_tiles = new List<GenericWeightable<Tile>>();

        foreach(Tile t in from_tiles)
        {
            float weight = 30 / Vector3.Distance( target.transform.position, t.transform.position);

            int tiles_ahead = Mathf.Max(0, t.TilePos.z - m_unit.currentTile.TilePos.z);

            weight += tiles_ahead * 2;
            weight /= Mathf.Max(1, t.CrumbleStage * 5);

            weighted_tiles.Add(new GenericWeightable<Tile>(weight,t));
            //Debug.Log(weight);
        }

        Debug.Log("choosing from " + weighted_tiles.Count);
        return WeightableFactory.GetWeighted (weighted_tiles).Value;

    }


    IEnumerator Decide()
    {
        if (m_Actions.IsActionInProgress) yield return null;

        if (Triggered)
        {
            UnitAction_ApplyEffectFromWeapon attack = getAttack();

            if (preferred_target == null || preferred_target.IsDead())
            {
                preferred_target = FindPreferredTarget();
            }

            Dictionary<Unit, List<Tile>> map = GetEnemiesAttackableWithin1Move();
            
            if(attack.CanTarget(preferred_target))
            {
                if (!m_Actions.HasAP(2)) { 
                     yield return StartCoroutine(Attack(preferred_target));
                } else
                {
                    yield return StartCoroutine(Move(GetBestAttackPosition(preferred_target, map[preferred_target])));
                }

            } else
            {
                if (map.ContainsKey(preferred_target))
                {
                    if (Random.value < Constants.AI_TARGET_SWITCH_WHEN_OUT_OF_ATTACK_RANGE)
                    {
                        preferred_target = FindPreferredTarget();
                    }
                    yield return StartCoroutine(Move(GetBestAttackPosition(preferred_target, map[preferred_target])));
                } else
                {
                    if(Random.value < Constants.AI_TARGET_SWITCH_WHEN_OUT_OF_MOVE_ATTACK_RANGE)
                    {
                        preferred_target = FindPreferredTarget();
                    }

                    List<Tile> move_to_tiles = map.ContainsKey(preferred_target) ? map[preferred_target] : GetWalkableTiles(getMove());

                    yield return StartCoroutine(Move(GetBestAttackPosition(preferred_target, move_to_tiles)));
                }
            }  
        } else if(m_Actions.HasAP(2) && Random.value < Constants.AI_PATROL_CHANCE)
        {
            yield return StartCoroutine(MovePatrol());
        } else
        {
            SkipTurn();
        }

    }


    void SkipTurn()
    {
       // Debug.Log("ai skip turn");
        m_unit.SkipTurn();
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

    public void OnTrigger(Unit triggerer)
    {
        
        preferred_target = triggerer;
        m_unit.Activate();
        Cover.SetActive(false);
      //  Debug.Log(m_unit.GetID() + " now attacking");
        Triggered = true;

        TriggerUnitsForGroup(this, triggerer);
    }

    public static void TriggerUnitsForGroup(UnitAI unit, Unit triggerer)
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
                u.GetComponent<UnitAI>().OnTrigger(triggerer);
            }
            }
        );
    }
}
