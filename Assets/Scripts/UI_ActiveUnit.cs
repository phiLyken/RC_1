using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UI_ActiveUnit : MonoBehaviour {

    public Image Portrait;
    public UI_EffectListView_Active effect_list_view;
    public GameObject Blocker;
    CanvasGroup Group;

    public Text SelectedUnitTF;


    public static UI_ActiveUnit Instance;
    void Awake()
    {
        Group = GetComponent<CanvasGroup>();
        Instance = this;
        Unit.OnTurnStart += SetActiveUnit;

    }

    void Start()
    {
        TurnSystem.Instance.OnGlobalTurn += Hide;
    }

    void Hide(int i)
    {
        Group.alpha = 0;
        Group.interactable = false;
    }
    void OnDestroy()
    {
        Unit.OnTurnStart -= SetActiveUnit;

        if (TurnSystem.Instance!=null)
         TurnSystem.Instance.OnGlobalTurn -= Hide;
    }
    public void SetActiveUnit(Unit unit)
    {
        Sequence _fade = DOTween.Sequence();

        if(unit.OwnerID == 0)
        {
            SelectedUnitTF.text = unit.GetID();
            //rank.sprite = unit.Config.
            _fade.Append(Group.DOFade(1, 0.5f));
            Blocker.SetActive(false);
            Group.interactable = true;
            Portrait.sprite = unit.GetIcon();

        } else
        {
            Blocker.SetActive(true);
            _fade.Append(Group.DOFade(0, 0.5f));
            Group.interactable = false;
            
        }
      
   


    }
}
