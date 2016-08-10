using UnityEngine;
using System.Collections;

public class ToggleActiveOnTurn : MonoBehaviour
{

    public GameObject Target;

    public void SetUnit(Unit unit)
    {

        transform.SetParent(unit.transform);
        transform.localPosition = Vector3.zero;

        Target.SetActive(false);

        Unit.OnTurnStart += u =>
        {
            if (u == unit)
                Target.SetActive(true);
        };

        Unit.OnTurnEnded += u =>
        {
            if (u == unit)
                Target.SetActive(false);
        };
    }


}
