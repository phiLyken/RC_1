using UnityEngine;
using System.Collections;

public abstract class TileComponent : MonoBehaviour {

    public enum TileComponents
    {
        loot, foo
    }

    protected TileComponents Type;

    public abstract TileComponents GetComponentType();

}
