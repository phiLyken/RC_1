using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_SquadSlot : GenericView<Unlockable<SquadSizeConfig>> {

  

    ScriptableUnitConfig m_config;

    public GameObject Available;
    public GameObject Locked;
    public GameObject Filled;

    public UniqueSelectionGroup<GameObject> States;


    public static UI_SquadSlot MakeSlot(Unlockable<SquadSizeConfig> config, Transform target)
    {
        GameObject prefab = Resources.Load("UI/ui_squad_slot") as GameObject;
        UI_SquadSlot slot = prefab.Instantiate<UI_SquadSlot>(target, true);

        slot.SetItem(config);

        return slot;

       
    }

    public override void Remove()
    {
    }

    public override void Updated()
    {
        setState( );
    }

    protected override void OnSet(Unlockable<SquadSizeConfig> item)
    {
        States = new UniqueSelectionGroup<GameObject>();         
        States.Init(new List<GameObject>() { Available, Locked, Filled }, state => state.SetActive(false), state => state.SetActive(true));

        setState( );
    }
    public void SetUnit(ScriptableUnitConfig unit)
    {
        m_config = unit;
        setState();
    }
    void setState( ) 
    {

        if(m_config != null)
        {
            States.Select(Filled);

        } else if (m_Item.IsUnlocked())
        {
            States.Select(Available);
        } else
        {
            States.Select(Locked);
        } 
    }

    
    
}
