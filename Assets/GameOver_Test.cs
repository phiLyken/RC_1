using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOver_Test : MonoBehaviour {

    public List<ScriptableUnitConfig> killed;
    public List<ScriptableUnitConfig> evac;

    public Item_Generic item;
    public GameObject Popup;

    public void Awake()
    {
        SquadManager.Instance.killed = killed;
        SquadManager.Instance.evacuated = evac;

        PlayerInventory.Instance.AddItem(item, 1000);

        Popup.SetActive(true);
    }
}
