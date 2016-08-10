using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ActiveUnit : MonoBehaviour {

    public UI_InventoryView inventory_view;
    public UI_EffectListView effect_list_view;

    public Text SelectedUnitTF;


    public static UI_ActiveUnit Instance;
    void Awake()
    {
        Instance = this;
        Unit.OnTurnStart += SetActiveUnit;
    }

    public void SetActiveUnit(Unit unit)
    {
       SelectedUnitTF.text = unit.GetID();
       inventory_view.SetInventory(unit.GetComponent<UnitInventory>());
       effect_list_view.SetEffects(unit.GetComponent<Unit_EffectManager>());


    }
}
