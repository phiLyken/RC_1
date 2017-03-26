using UnityEngine;
using System.Collections;

public class UnlockableViewTest : MonoBehaviour {

    public void SetUnlockable(Unlockable<BlaConfig> unlockables)
    {
        MDebug.Log("SET @" + unlockables.Item.requirement);
        unlockables.AddUnlockListener(Foo);
    }

    public void Foo(BlaConfig bla)
    {
        MDebug.Log(bla.requirement.ToString());
    }
}
