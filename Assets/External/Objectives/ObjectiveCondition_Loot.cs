using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ObjectiveCondition_Loot : ObjectiveCondition {

    public GameObject ArrowPrefab;
    GameObject _spawned_arrow;

    public ItemTypes type;
    public int LootAmount;

    int count;
 

    bool wait_for_hint;
    Unit m_Unit;
    GameObject hint;

    Tile_Loot m_Loot;

    public override void SetActive()
    {
        GlobalUpdateDispatcher.OnUpdate += _update;
    }
    void _update(float f)
    {
        if(m_Unit == null)
        {
            m_Unit = Unit.GetAllUnitsOfOwner(0, true).FirstOrDefault();
            if(m_Unit != null)
            {
                m_Unit.Inventory.OnInventoryUpdated += OnLoot;
                
               
            }
        }

        if (hint == null && canLoot() && !wait_for_hint)
        {
            StartCoroutine(DelayedHint());
        }

        if(m_Loot == null && ArrowPrefab != null)
        {
           m_Loot = GameObject.FindObjectOfType<Tile_Loot>();
           if(m_Loot != null)
            {
                _spawned_arrow = ArrowPrefab.Instantiate(m_Loot.transform, true);
            }
        }
    }

    

    IEnumerator DelayedHint()
    {
        wait_for_hint = true;
        yield return new WaitForSeconds(5f);

        hint = UI_Prompt.MakePrompt(
                          FindObjectsOfType<UI_ActionBar_ButtonAnchor>().Where(btn => btn.ButtonID == ActionButtonID.loot).First().transform as RectTransform,
                          "Select the LOOT ability.\nThen click on the tile with loot.", 2,
                          delegate {
                              return !canLoot();
                          },
                       true).gameObject;

        wait_for_hint = false;

    }

    void OnLoot(IInventoryItem item, int count)
    {
        if(item.GetItemType() == type)
        {
            count += count;
            if(count >= LootAmount)
            {
                if(_spawned_arrow != null)
                {
                    GameObject.Destroy(_spawned_arrow);
                }
                m_Unit.Inventory.OnInventoryUpdated -= OnLoot;
                GlobalUpdateDispatcher.OnUpdate -= _update;
                Complete();
               
            }
        }
    }

    bool canLoot()
    {
        return (m_Unit != null) && m_Unit.Actions.GetActionOfType<UnitAction_Loot>().CanExecAction(false);
        
    }
}
