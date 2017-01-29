using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompasGroup : MonoBehaviour {

    UniqueSelectionGroup<GameObject> group;

    public List<GameObject> _arrows;


    void Awake()
    {
        group = new UniqueSelectionGroup<GameObject>();
        group.Init(_arrows, UnSelect, OnSelect);
    }

    void OnSelect(GameObject obj)
    {
        obj.SetActive(true);
    }

    void UnSelect(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SelectIndex(int i)
    {
        group.Select(_arrows[i]);
    }
}
