using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetLazer : MonoBehaviour {

    public static SetLazer MakeLazer(float duration, List<Vector3> positions, Color c )
    {
        GameObject go = Resources.Load ( "lazer_base" ) as GameObject;
        return Instantiate(go).GetComponent<SetLazer>().SetLaser(duration, positions, c);
        
    }

    public SetLazer SetLaser(float duration, List<Vector3> v3l, Color c)
    {
        if(v3l == null || v3l.Count < 2)
        {
            return null;
        }

        LineRenderer line = GetComponent<LineRenderer>();

        line.SetVertexCount(v3l.Count);

        for (int i = 0; i < v3l.Count; i++)
        {
            line.SetPosition(i, v3l[i]);
        }

        line.SetColors(c, c);

        DestroyAfterTime dt = GetComponent<DestroyAfterTime>();
        dt.lifetime = duration;
        dt.StartTimer();

        return this;

    }
}
