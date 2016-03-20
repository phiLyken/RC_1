using UnityEngine;
using System.Collections;

public interface IActionPreview
{

    void OnSelectAction(Unit unit);
    void OnUnselect(Unit unit);

}
