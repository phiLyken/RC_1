using UnityEngine;
using System.Collections;

public class MDebug_DrawForm : MonoBehaviour {

    public enum DebugShapes
    {
        sphere, cube, wire_sphere, wire_cube
    }

    public DebugShapes Shape;
    public float Size;
    public Color Color;

    void OnDrawGizmos()
    {

    }
}
