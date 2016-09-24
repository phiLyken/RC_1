using UnityEngine;
using System.Collections;

public class TargetHighLight : MonoBehaviour
{
    public GameObject Hovered;

    void Awake()
    {
        Hovered.SetActive(false);
    }

}