using UnityEngine;
using System.Collections;

public class MDebug_DrawRay : MonoBehaviour {

    public Color Color;

    public enum Direction
    {
        x, y, z
    }

    public Direction Axis;
    public float Lenth;

    void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Vector3 ray = Vector3.zero;

        switch (Axis)
        {
            case Direction.x:
                ray = transform.right* Lenth;
                break;
            case Direction.y:
                ray = transform.up * Lenth;
                break;
            case Direction.z:
                ray = transform.forward * Lenth;
                break;

        }
   
        Debug.DrawRay(transform.position, ray, Color);
    }

}
