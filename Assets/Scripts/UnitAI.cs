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
        Unit.OnTurnStart += StartTurn;
        startTile = m_unit.currentTile;

        m_unit.OnDamageReceived += dmg =>
        {
            OnTrigger(dmg.Instigator as Unit);
        };

      
    }

    void StartTurn(Unit u)
    {     
        if(u == m_unit)
            StartCoroutine(AISequence());
    }

    UnitAction_ApplyEffectFromWeapon getAttack()
    {
        return m_Actions.GetActionOfType<UnitAction_ApplyEffectFromWeapon>();
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
        return m_Actions.GetActionOfType<UnitAction_Move>();
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
        yield return null;
        target.OnHoverEnd() ;
    }
    IEnumerator MovePatrol()
    {
        Tile patrolDest = GetPatrolTile(m_unit.currentTile);
        Debug.Log("Patrol");
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
    List<Tile> GetTilesForAttack(Unit target, List<Tile> tiles_to_check)
    {
        UnitAction_ApplyEffectFromWeapon atk = getAttack();
        List<Tile> tiles = new List<Tile>();


        foreach (Tile t in tiles)
        {
            if(atk.CanTarget(target, t))
            {
                tiles.Add(t);
            }
        }

        return tiles;
           
    }
    
    Dictionary<Unit, List<List<Tile>>> GetPathMapForTargets()
    {
        List<Unit> enemy_group = Unit.GetAllUnitsOfOwner(0, false);
        Dictionary<Unit, List<List<Tile>>> map = new Dictionary<Unit, List<List<Tile>>>();

        foreach (Unit enemy in enemy_group)
        {
            List<List<Tile>> paths = GetPathsToAttackZone(enemy);
            if(paths.Count > 0)
            {
                map.Add(enemy, paths);
            }
        }

        return map;

    }
    List<List<Tile>> GetPathsToAttackZone(Unit target)
    {
        List<List<Tile>> path_list = new List<List<Tile>>();
        List<Tile> tiles = GetTilesFromWhichUnitCanAttack(target);
        foreach ( Tile t in tiles)
        {
            List<Tile> path = TileManager.Instance.FindPath(m_unit.currentTile, t, m_unit);
            if(path.HasItems())
            {
                path_list.Add(path);
            }
        }
 

        return path_list;
    }

    Dictionary<Unit, List<Tile>> GetEnemyAttackTileMap()
    {
        Dictionary<Unit, List<Tile>> unit_map = new Dictionary<Unit, List<Tile>>();
        List<Unit> enemy_group = Unit.GetAllUnitsOfOwner(0, false);

        foreach(Unit enemy in enemy_group)
        {
            List<Tile> attack_tiles = GetTilesFromWhichUnitCanAttack(enemy);
            if(attack_tiles.HasItems() )
            {
                unit_map.Add(enemy, attack_tiles);
            }

        }
        return unit_map;       
    }
 
    List<Tile>  GetTilesFromWhichUnitCanAttack(  Unit target)
    {
        UnitAction_ApplyEffectFromWeapon atk = getAttack();
        
        List<Tile> tiles = TileManager.Instance.GetTilesInRange(target.currentTile, atk.GetRange());
        List<Tile> can_attack = tiles.Where(tile => atk.CanTarget(target, tile)).ToList();
        return can_attack;
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
        while ( !(m_unit).HasEndedTurn()  ){
            
            if(! m_Actions.IsActionInProgress && m_Actions.HasAP(1))
                yield return StartCoroutine( Decide());

            yield return null;
        }
       // Debug.Log("Ai ended");

    }

    Unit FindPreferredTarget()
    {
    return  MyMath.GetRandomObject(Unit.GetAllUnitsOfOwner(0, true));
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

        if(weighted_tiles.Count == 0)
        {
            Debug.LogError("Could not find attack tiles");
            return null;
        }
       
        return WeightableFactory.GetWeighted (weighted_tiles).Value;

    }

    IEnumerator AttackOrMove()
    {
        Debug.Log("What to do..`?");

        UnitAction_ApplyEffectFromWeapon attack = getAttack();
        UnitAction_Move move = getMove();

        //dictionary of all units and the paths by which they are attackable
        Dictionary<Unit, List<List<Tile>>> attack_map = GetPathMapForTargets();

        List<Unit> units_attackable = Unit.GetAllUnitsOfOwner(0, false).Where(unit => attack.CanTarget(unit)).ToList();

        List<Unit> units_move_attackable = attack_map.Where(kvp => CanFinishPathInMoves(1, kvp.Value)).Select(kvp => kvp.Key).ToList();
        List<Unit> units_movable = attack_map.Where(kvp => !units_move_attackable.Contains(kvp.Key)).Select(kvp => kvp.Key).ToList();
              

        if(attack_map.Count > 0 && ( preferred_target == null || !preferred_target.IsActive))
        {
            preferred_target = MyMath.GetRandomObject(attack_map.Select(kvp => kvp.Key).ToList());
        }

        if(preferred_target == null)
        {
            yield return StartCoroutine(MoveRandom());
            yield break;
        } 
           
        if (!units_attackable.Contains(preferred_target) && (!units_attackable.IsNullOrEmpty() && ( !attack_map.ContainsKey(preferred_target) || Random.value < Constants.AI_TARGET_SWITCH_WHEN_OUT_OF_ATTACK_RANGE)))
        {
            preferred_target = MyMath.GetRandomObject(units_attackable );
        }

        if (attack.CanTarget(preferred_target))
        {
            Tile better_pos =  GetReachableAttackPosition(preferred_target);
             
            if (m_Actions.HasAP(2) && better_pos != null)
            {
                yield return StartCoroutine(Move(better_pos));
                yield break;
            }
            else
            {
                yield return StartCoroutine(Attack(preferred_target));
                yield break;
            }
        }
        else
        {
            List<Unit> other_move_targets = units_move_attackable.Where(unit => unit != preferred_target && !units_attackable.Contains(unit)).ToList();

            if (!other_move_targets.IsNullOrEmpty()   && Random.value < Constants.AI_TARGET_SWITCH_TO_ATTACK_MOVE_WHEN_OUT_OF_MOVE_ATTACK_RANGE)
            {
                preferred_target = MyMath.GetRandomObject(other_move_targets);
            }
            else
            {
                if (!attack_map.ContainsKey(preferred_target) && !units_movable.IsNullOrEmpty() && Random.value < Constants.AI_TARGET_SWITCH_TO_CHASE_WHEN_OUT_OF_ATTACK_RANGE)
                {
                    preferred_target = MyMath.GetRandomObject(units_movable);
                }

            }

            if (attack_map.ContainsKey(preferred_target))
            {
                if (units_move_attackable.Contains(preferred_target)){

                    Tile attack_pos = GetReachableAttackPosition(preferred_target);

                    if(attack_pos != null)
                    { 
                        yield return StartCoroutine(Move(attack_pos));
                    } else
                    {
                        yield return StartCoroutine(MoveRandom());
                    }

                    yield break;
                } else 
                {
                    Tile Final_Target_Tile = GetBestAttackPosition(preferred_target, attack_map[preferred_target].Select(path => path[path.Count - 1]).ToList());

                    List<Tile> final_path = TileManager.Instance.FindPath(m_unit.currentTile, Final_Target_Tile, m_unit);

                    Tile target = move.GetFurthestMovibleTileOnPath(final_path);

                    yield return StartCoroutine(Move(target));
                    yield break;
                }
            } else
            {
                yield return StartCoroutine(MoveRandom( ));
                yield break;
            }
        }

    }

    Tile GetReachableAttackPosition(Unit target)
    {
        List<Tile> attack_tiles = GetTilesFromWhichUnitCanAttack(target);
        List<Tile> walkable_tiles = GetWalkableTiles(getMove());

        if ( attack_tiles.IsNullOrEmpty() || walkable_tiles.IsNullOrEmpty())
            return null;

        return GetBestAttackPosition(target, attack_tiles.Where(atk_tile => walkable_tiles.Contains(atk_tile)).ToList());
    }

    bool CanFinishPathInMoves(int moves,   List<List<Tile>> paths)
    {
        int range = moves * getMove().GetMoveRange();

        foreach(List<Tile> path in paths)
        {
            if (TilePathFinder.GetPathLengthForUnit(m_unit, path)  <= range)
                return true;
        }
        return false;

    }
    
    IEnumerator Decide()
    {
       
        if (!m_Actions.HasAP(1))
            yield return null;

        if (Triggered)
        {
            yield return StartCoroutine(AttackOrMove());
           
        } else if(m_Actions.HasAP(2) && Random.value < Constants.AI_PATROL_CHANCE && m_unit.Stats.GetStatAmount(StatType.move_range) > 0)
        {
           
            yield return StartCoroutine(MovePatrol());
        } else
        {
           
            SkipTurn();
            yield return null;
        }

    }


    void SkipTurn()
    {
        Debug.Log("ai skip turn");
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
