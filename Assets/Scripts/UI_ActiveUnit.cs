using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ActiveUnit : MonoBehaviour {

    public UI_InventoryView inventory_view;
    public Text SelectedUnitTF;
    public Text AbilityTF;

    public static UI_ActiveUnit Instance;
    void Awake()
    {
        Instance = this;
    }

    public void SetActiveUnit(Unit unit)
    {
       SelectedUnitTF.text = unit.GetID();
       inventory_view.SetInventory(unit.GetComponent<UnitInventory>());
    }
}
