using UnityEngine;
using System.Collections;
using System.Linq;

public class ObjectiveCondition_ReachCamp : ObjectiveCondition {

    GameObject _spawnedArrow;

    public GameObject Arrow;

    public override void SetActive()
    {
        base.SetActive();

        Unit playerunit = Unit.GetAllUnitsOfOwner(0, true).FirstOrDefault();

        if(playerunit != null)
        {
             playerunit.Actions.GetActionOfType<UnitAction_Move>().OnActionComplete += OnMoveEnd;

            GlobalUpdateDispatcher.OnUpdate += _update;
        }
    }

    void _update(float time)
    {
        if(_spawnedArrow == null && Arrow != null)
        { 
            Vector3 center = TileManager.Instance.GetTileList().Where(t => t.isCamp).Select(t => t.transform).ToList().Center();
            Transform parent = M_Math.GetClosestGameObject(center, TileManager.Instance.GetTileList().Where(t => t.isCamp).Select(t => t.gameObject).ToArray()).transform;

            _spawnedArrow = Arrow.Instantiate(parent, true);
         
        }
    }
    void OnMoveEnd(UnitActionBase b)
    {
        if (b.GetOwner().currentTile.isCamp) {

            b.OnActionComplete -= OnMoveEnd;
            GlobalUpdateDispatcher.OnUpdate -= _update;
            if (_spawnedArrow != null)
                    Destroy(_spawnedArrow);
            Complete();
        }
    }

  
}
