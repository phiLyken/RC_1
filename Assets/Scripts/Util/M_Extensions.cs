using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

public static class M_Extensions   {


    public static void DeleteChildren(this GameObject obj)
    {

        for (int i = obj.transform.childCount - 1; i >= 0; i--)
        {
            MonoBehaviour.DestroyImmediate(obj.transform.GetChild(i).gameObject);
        }
    }


    public static int Direction(this float f)
    {
        return (int) (f / Mathf.Abs(f));
    }

    public static void DetachChildren(this Transform tr)
    {
        DetachChildren(tr, null);
    }
    public static void DetachChildren(this Transform tr, Transform target)
    {
        List<Transform> _children = new List<Transform>();
        foreach (Transform transform in tr)
        {
            _children.Add(transform);
          //  Debug.Log("detach " + transform.gameObject.name);
        }

        _children.ForEach(child => child.SetParent(tr));
    }

    /// <summary>
    /// pauses emission on all attached particles systems
    /// </summary>
    /// <param name="particle_system"></param>
    public static void StopAll(this ParticleSystem particle_system)
    {
        List<ParticleSystem> systems = new List<ParticleSystem>();
        systems.Add(particle_system);
        systems.AddRange(particle_system.GetComponentsInChildren<ParticleSystem>().ToList());

        systems.ForEach(sys => sys.PauseEmission());

    }

    /// <summary>
    /// removes the toplevel gameobject when all child particles (and the one passed as ´param) are inactive
    /// </summary>
    /// <param name="system"></param>
    public static void RemoveAllParticlesWhenInactive(this ParticleSystem system)
    {
        system.gameObject.AddComponent<RemoveParticles>();
    }
    public static void PauseEmission(this ParticleSystem system)
    {

        var em = system.emission;
        em.enabled = false;


    }

    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void CountToInt(this Text TF, int from, int to, float time)
    {
        TF.StartCoroutine(CountTo(from, to, Mathf.Max(0, time), counted => TF.text = counted.ToString()));
    }

    public static IEnumerator CountTo(int from, int to, float time, Action<int> funct)
    {
        float t = 0;
        while (t < 1)
        {
            funct((int) Mathf.Lerp(from,   to, t));
            t += Time.deltaTime / time;
            yield return null;
        }

        funct(to);
        
    }

    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        return list == null || list.Count == 0;
    }

    public static bool HasItems<T>(this List<T> list)
    {
        return list != null && list.Count > 0;
    }

    public static bool HasItems<T,Z>(this Dictionary<T,Z> dictionary)
    {
        return dictionary != null && dictionary.Count > 0;
    }

    public static bool IsNullOrEmpty<T, Z>(this Dictionary<T, Z> dictionary)
    {
        return dictionary == null || dictionary.Count ==0;
    }

 
}
