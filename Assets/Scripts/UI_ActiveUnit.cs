using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UI_ActiveUnit : MonoBehaviour {

    public UI_InventoryView inventory_view;
    public UI_EffectListView_Active effect_list_view;

    CanvasGroup Group;

    public Text SelectedUnitTF;


    public static UI_ActiveUnit Instance;
    void Awake()
    {
        Group = GetComponent<CanvasGroup>();
        Instance = this;
        Unit.OnTurnStart += SetActiveUnit;
    }

    void OnDestroy()
    {
        Unit.OnTurnStart -= SetActiveUnit;
    }
    public void SetActiveUnit(Unit unit)
    {
        Sequence _fade = DOTween.Sequence();

        if(unit.OwnerID == 0)
        {
            SelectedUnitTF.text = unit.GetID();
            inventory_view.SetInventory(unit.GetComponent<UnitInventory>());
            _fade.Append(Group.DOFade(1, 0.5f));

        } else
        {
            _fade.Append(Group.DOFade(0, 0.5f));
        }
      
   


    }
}
