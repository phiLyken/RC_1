﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Text;
 
public delegate void BoolEventHandler(bool b);
public delegate void IntEventHandler(int _int);
public delegate void FloatEventHandler(float f);
public delegate int GetInt(bool b);
public delegate bool GetBool();

public static class M_Extensions
{
    public static T GetOrAddComponent<T> (this GameObject _gameObject) where T : Component
    {
        T comp = _gameObject.GetComponent<T>();

        if( ReferenceEquals( default(T), comp))
        {
            comp = _gameObject.AddComponent<T>();
        }

        return comp;
    }
    public static Dictionary<Key, Value> MakeDictionairy<Key, Value, FromItem>(this List<FromItem> items, Func<FromItem, Key> getKey, Func<FromItem, Value> getValue)
    {
        Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
        foreach (var item in items)
        {
            dict.Add(getKey(item), getValue(item));
        }

        return dict;
    }

    public static Color ParseHexToColor (this string to_parse)
    {
        if(string.IsNullOrEmpty(to_parse) || to_parse.Length != 8)
        {
            Debug.LogWarning("NO GOOD HEX STRING");
            return Color.magenta;
            
        }

        byte red =  Convert.ToByte(to_parse.Substring(0, 2), 16);
        byte green = Convert.ToByte(to_parse.Substring(2, 2), 16);
        byte blue = Convert.ToByte(to_parse.Substring(4, 2), 16);
        byte alpha = Convert.ToByte(to_parse.Substring(6, 2), 16);

        return new Color32(red, green, blue, alpha);
     
    }
    public static T MakeMonoSingletonFromPrefab<T>(out T _save_to) where T : MonoBehaviour, IInit
    {
        {
            T existing = GameObject.FindObjectOfType<T>();

            if (existing != null)
            {
                _save_to = existing;
            }
            else
            {
                GameObject obj = GameObject.Instantiate(Resources.Load(GenericSingletonToPrefabMap.GetPrefab<T>())) as GameObject;
                _save_to = obj.GetComponent<T>();
            }
            GameObject.DontDestroyOnLoad(_save_to);
            _save_to.Init();

            return _save_to;
        }
    }

    public static T MakeMonoSingleton<T>(out T _save_to, GameObject prefab) where T : MonoBehaviour, IInit
    {
        {
            T existing = GameObject.FindObjectOfType<T>();

            if (existing != null)
            {
                _save_to = existing;
            }
            else
            {
                GameObject obj = GameObject.Instantiate(prefab) as GameObject;
                _save_to = obj.GetComponent<T>();
            }
            GameObject.DontDestroyOnLoad(_save_to);
            _save_to.Init();

            return _save_to;
        }
    }

    public static T MakeMonoSingleton<T>(out T _save_to) where T : MonoBehaviour, IInit
    {
        T existing = GameObject.FindObjectOfType<T>();

        if (existing != null)
        {
            _save_to = existing;
        }
        else
        {
            GameObject obj = new GameObject("_SINGLETON_" + typeof(T).ToString());
            _save_to = obj.AddComponent<T>();
        }
        GameObject.DontDestroyOnLoad(_save_to);
        _save_to.Init();

        return _save_to;
    }
    public static IEnumerator Blink(this GameObject obj, float time_1, float time_2, int blinks, int last_state, bool start, bool end)
    {
        bool show = start;

        obj.SetActive(show);

        for(int i = 0; i < blinks; i++)
        {
            yield return new WaitForSeconds(i % 2 == 0 ? time_1 : time_2);
            show = !show;
            obj.SetActive(show);
        }

        obj.SetActive(end);
    }
    /// <summary>
    /// DestroyImmidiate on all children of the object
    /// </summary>
    /// <param name="obj"></param>
    public static void DeleteChildren(this GameObject obj)
    {

        for (int i = obj.transform.childCount - 1; i >= 0; i--)
        {
            MonoBehaviour.DestroyImmediate(obj.transform.GetChild(i).gameObject);
        }
    }

    public static void SetUIAlpha(this GameObject _object, float alpha)
    {
        CanvasGroup c = _object.GetComponent<CanvasGroup>();
        if(c == null)
        {
            c = _object.AddComponent<CanvasGroup>();
        }
        c.alpha = alpha;
    }
    public static void AttemptCall(this System.Action action)
    {
        if (action != null)
            action();
    }

    public static void AttemptCall(this System.Action action, string debug)
    {
        MDebug.Log(debug);
        AttemptCall(action);
    }
    public static void AddOrUpdate<T, V>(this Dictionary<T, V> dictionairy, T newKey, V newValue)
    {
        if (dictionairy.ContainsKey(newKey))
        {
            dictionairy[newKey] = newValue;
        }
        else
        {
            dictionairy.Add(newKey, newValue);
        }
    }
    public static V GetAndRemove<T, V>(this Dictionary<T, V> dictionairy, T Key)
    {
        if (dictionairy.ContainsKey(Key))
        {
            V value = dictionairy[Key];
            dictionairy.Remove(Key);
            return value;
        }

        return default(V);
    }

    public static void AttemptRemove<T, V>(this Dictionary<T, V> dictionairy, T key)
    {
        if (dictionairy.ContainsKey(key))
        {
            dictionairy.Remove(key);
        }

    }

    public static void AttemptCall<T>(this Action<T> action, T value, string debug)
    {
        MDebug.Log(debug);
        AttemptCall(action, value);
    }

    public static void AttemptCall<T>(this Action<T> action, T value)
    {
        if (action != null)
        {
            action(value);
        }
    }

    public static Rect Move(this Rect rect, float x, float y)
    {
        rect.position += new Vector2(x, y);
        return rect;
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
            //  MDebug.Log("detach " + transform.gameObject.name);
        }

        _children.ForEach(child => child.SetParent(tr));
    }

    /// <summary>
    /// pauses emission on all attached particles systems
    /// </summary>
    /// <param name="particle_system"></param>
    public static void SetEmissionAll(this ParticleSystem particle_system, bool b)
    {
        List<ParticleSystem> systems = new List<ParticleSystem>();
        systems.Add(particle_system);
        systems.AddRange(particle_system.GetComponentsInChildren<ParticleSystem>().ToList());

        systems.ForEach(sys => {
            sys.SetEmission(b);
            sys.loop = false;
        });

    }
    /// <summary>
    /// pauses emission on all attached particles systems
    /// </summary>
    /// <param name="particle_system"></param>
    public static void StopAll(this ParticleSystem particle_system)
    {
        particle_system.SetEmissionAll(false);

    }
    /// <summary>
    /// Moves the transform by a clamped move from a reference point. 
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="bounds"></param>
    /// <param name="move"></param>
    public static void AttemptMoveInBounds(this Transform transform, Bounds bounds, Vector3 move, Vector3 reference)
    {
        Vector3 targetCenterPos = reference + move;

        Debug.DrawLine(reference, targetCenterPos, Color.magenta);

        Vector3 clampedTargetCenterPos = M_Math.ClampInBounds(targetCenterPos, bounds);

        Debug.DrawLine(targetCenterPos, clampedTargetCenterPos, Color.red);

        move = clampedTargetCenterPos - reference;

        //StartCoroutine(M_Math.ExecuteDelayed(0.05f, () => Debug.Break()));
        Debug.DrawRay(reference, move, Color.yellow);

        //   MDebug.Log(move);
        transform.Translate(move, Space.World);

    }

    /// <summary>
    /// Translates a transform only if its own bounds are intersecting with the target bounds
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="_move"></param>
    /// <param name="_bounds"></param>
    public static void Translate(this Transform _transform, Vector3 _move, Bounds _bounds, Space space)
    {
        if (CanMoveWithinBounds(_transform, _move, _bounds))
        {
            _transform.Translate(_move, space);
        }
    }

    /// <summary>
    /// Translates a transform only if its own bounds are intersecting with the target bounds
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="_move"></param>
    /// <param name="_bounds"></param>
    public static void Translate(this Transform _transform, Vector3 _move, Bounds _bounds)
    {
        _transform.Translate(_move, _bounds, Space.World);
    }

    /// <summary>
    /// Returns whether the transforms bounds are intersecting with target bounds when moving
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="_move"></param>
    /// <param name="_bounds"></param>
    public static bool CanMoveWithinBounds(this Transform _transform, Vector3 _move, Bounds bounds)
    {
        Bounds tr_bound = _transform.Bounds();
        tr_bound.center += _move;
        return _transform.Bounds().Intersects(tr_bound);
    }

    public static bool IsInBounds(this Vector3 position, Bounds b)
    {

        return b.Contains(position);
    }

    public static bool IsInBounds(this Vector3 position, Transform b)
    {
        return b == null || position.IsInBounds(b.Bounds());
    }

    public static Vector3 Center(this List<Transform> transforms)
    {
        Vector3 center = transforms[0].position;
        for(int i = 1; i < transforms.Count; i++)
        {
            center += transforms[i].position;
        }

       return  center /= transforms.Count;
     
    }
    /// <summary>
    /// Returns the bounds of the transform and all of its 1st level children
    /// </summary>
    /// <param name="tr"></param>
    /// <returns></returns>
    public static Bounds Bounds(this Transform tr)
    {

        // First find a center for your bounds.
        Vector3 center = tr.position;

        foreach (Transform child in tr.transform)
        {
            center += child.transform.position;
        }
        center /= (tr.transform.childCount + 1); //center is average center of children

        //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
        Bounds bounds = new Bounds(center, Vector3.zero);
        bounds.Encapsulate(new Bounds(tr.transform.position, tr.transform.localScale));

        foreach (Transform child in tr.transform)
        {
            bounds.Encapsulate(new Bounds(child.transform.position, child.transform.localScale));
        }
        //  MDebug.Log(bounds.extents+ " " + bounds.size.ToString());
        return bounds;
    }

    /// <summary>
    /// Sets the transforms position and scale to the extents and position of the bounds
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="bounds"></param>
    public static void SetTransformToBounds(this Transform tr, Bounds bounds)
    {
        tr.position = bounds.center;
        tr.localScale = bounds.size;
    }
    public static T MakeNew<T>(string GameobjectName) where T : MonoBehaviour
    {
        return new GameObject(GameobjectName, typeof(T)).GetComponent<T>();
    }


    public static Component MakeNew(Type behavior)
    {
        return new GameObject("__", behavior).GetComponent(behavior);
    }

    /// <summary>
    /// pauses emission on all attached particles systems
    /// </summary>
    /// <param name="particle_system"></param>
    public static void ResumeAll(this ParticleSystem particle_system)
    {
        particle_system.SetEmissionAll(true);

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
        system.SetEmission(false);
    }

    public static void SetEmission(this ParticleSystem system, bool b)
    {
        var em = system.emission;
        em.enabled = b;
    }

    public static void ExecuteDelayed(this MonoBehaviour comp, float time, System.Action func)
    {
        comp.StartCoroutine(M_Math.ExecuteDelayed(time, func));
    }
    /// <summary>
    /// pauses emission on all attached particles systems
    /// </summary>
    /// <param name="particle_system"></param>
    public static void SetEmissionAll(this GameObject go, bool b)
    {
        List<ParticleSystem> systems = new List<ParticleSystem>();
        ParticleSystem _this = go.GetComponent<ParticleSystem>();
        if (_this != null)
        {
            systems.Add(_this);
        }
        systems.AddRange(go.GetComponentsInChildren<ParticleSystem>().ToList());

        systems.ForEach(sys => {
            sys.SetEmission(b);
            sys.loop = b;
        });

    }


    /// <summary>
    /// pauses emission on all attached particles systems
    /// </summary>
    /// <param name="particle_system"></param>
    public static void ToggleParticles(this GameObject go, bool b)
    {
        List<ParticleSystem> systems = new List<ParticleSystem>();
        ParticleSystem _this = go.GetComponent<ParticleSystem>();
        if (_this != null)
        {
            systems.Add(_this);
        }
        systems.AddRange(go.GetComponentsInChildren<ParticleSystem>().ToList());

        if (!b)
        {
            systems.ForEach(sys => sys.Stop());
        }
        else
        {
            systems.ForEach(sys => sys.Play());
        }


    }

    public static void ResumeEmission(this ParticleSystem system)
    {
        system.SetEmission(true);
    }

    public static void OneShotParticle(this ParticleSystem system, Vector3 position)
    {
        OneShotParticle(system.gameObject, position);
    }

    public static GameObject OneShotParticle(this GameObject prefab, Vector3 position)
    {
        GameObject newGo = GameObject.Instantiate(prefab, position, Quaternion.identity) as GameObject;
        newGo.AddComponent<RemoveParticles>();
        return newGo;
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


    public static T GetRandom<T>(this List<T> list)
    {
        if (!list.HasItems())
            return default(T);

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandomRemove<T>(this List<T> list)
    {
        if (!list.HasItems())
            return default(T);

        T item = list[UnityEngine.Random.Range(0, list.Count)];
        list.Remove(item);
        return item;
    }

    public static List<T> GetRandomRemove<T>(this List<T> list, int count)
    {
        if (!list.HasItems())
            return null;

        List<T> selected = new List<T>();

        while (list.HasItems() && selected.Count < count)
        {
            T item = list[UnityEngine.Random.Range(0, list.Count)];
            list.Remove(item);
            selected.Add(item);
        }
        
        return selected;
    }
    public static void CountToInt(this Text TF, int from, int to, float time)
    {
        TF.StartCoroutine(CountTo(from, to, Mathf.Max(0, time), counted => { if (TF != null) TF.text = counted.ToString(); }));
    }

    public static IEnumerator CountTo(int from, int to, float time, Action<int> funct)
    {
        float t = 0;
        while (t < 1)
        {
            funct((int) Mathf.Lerp(from, to, t));
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

    public static bool HasItems<T, Z>(this Dictionary<T, Z> dictionary)
    {
        return dictionary != null && dictionary.Count > 0;
    }

    public static bool IsNullOrEmpty<T, Z>(this Dictionary<T, Z> dictionary)
    {
        return dictionary == null || dictionary.Count == 0;
    }

    public static GameObject Instantiate(this GameObject prefab, Transform parent, bool set_to_parent_position)
    {
        if (prefab == null)
        {
            Debug.LogWarning("THE THING YOU WANT TO INSTANTIATE IS NULL!!");
            return null;
        }
        GameObject new_go = GameObject.Instantiate(prefab);
        new_go.transform.SetParent(parent,!set_to_parent_position);

        if (set_to_parent_position)
        {
            new_go.transform.position = parent.transform.position;
        }

        return new_go;
    }
    public static T Instantiate<T>(this UnityEngine.Object prefab, Transform parent, bool set_to_parent_position) where T : Component
    {
        if (prefab == null)
        {
            Debug.LogWarning("THE THING YOU WANT TO INSTANTIATE IS NULL!!");
            return null;
        }
        return (prefab as GameObject).Instantiate(parent, set_to_parent_position).GetComponent<T>();
    }
    public static GameObject Instantiate(this UnityEngine.Object prefab, Transform parent, bool set_to_parent_position)
    {
        if (prefab == null)
        {
            Debug.LogWarning("THE THING YOU WANT TO INSTANTIATE IS NULL!!");
            return null;
        }
        return (prefab as GameObject).Instantiate(parent, set_to_parent_position);
    }

    public static Transform FindChildWithTag(this Transform transform, string tag)
    {
        if (transform.CompareTag(tag))
        {
            return transform;
        }

        foreach (Transform child in transform)
        {
            var result = child.FindChildWithTag(tag);
            if (result != null)
                return result;
        }

        return null;
    }

    public static int RoundToNearest(this int i, int step)
    {
        return (int) Mathf.Round(i / step) * step;


    }

    public static void ChangeTint(this Image img, Color tint)
    {
        Color _base = img.color;
        tint.a = _base.a;

        img.color = tint;

    }

    public static void ChangeTint(this List<Image> imgs, Color tint)
    {

        foreach (var img in imgs)
            img.ChangeTint(tint);

    }

    public static Coroutine YieldT(this MonoBehaviour comp, Action<float> t, float time)
    {
        return comp.StartCoroutine(YieldT(t, time));
    }

    public static IEnumerator YieldT(Action<float> t, float time)
    {
        float _time = 0;
        while (_time < 1)
        {

            _time += Time.deltaTime / time;
            t(_time);
            yield return null;
        }

        t(1);
        yield break;
    }

    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    public static T GetAndRemove<T>(this List<T> list, int index)
    {
        if (list.IsNullOrEmpty())
        {
            return default(T);
        }

        T retreived = list[Mathf.Clamp(index, 0, list.Count - 1)];
        list.Remove(retreived);
        return retreived;
    }

    public static T GetLastAndRemove<T>(this List<T> list)
    {
        if (list.IsNullOrEmpty())
        {
            return default(T);
        }

        return list.GetAndRemove(list.Count - 1);
    }

}
