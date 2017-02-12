using UnityEngine;
using System.Collections;

public class UnlockableViewTest : MonoBehaviour {

    public void SetUnlockable(Unlockable<BlaConfig> unlockables)
    {
        Debug.Log("SET @" + unlockables.Item.requirement);
        unlockables.AddUnlockListener(Foo);
    }

    public void Foo(BlaConfig bla)
    {
        Debug.Log(bla.requirement);
    }
}
