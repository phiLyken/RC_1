using UnityEngine;
using System.Collections;

public class ToggleActiveOnTurn : MonoBehaviour
{

    public GameObject Target;

    public void SetUnit(Unit unit)
    {
        if (Target == null)
            Target = this.gameObject;

        transform.SetParent(unit.transform);
        transform.localPosition = Vector3.zero;

        Target.SetActive(false);

        Unit.OnTurnStart += u =>
        {
            if (u == unit && unit.IsIdentified)
                Target.SetActive(true);
        };

        Unit.OnTurnEnded += u =>
        {
            if (u == unit)
                Target.SetActive(false);
        };
    }


}
