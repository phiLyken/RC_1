using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Tile_Loot : TileComponent {

    public GameObject LootObject;
     Unit u;

    void Awake()
    {
        Type = TileComponents.loot;
        GetComponent<Tile>().OnSetChild += tile =>
        {
            Unit u = GetComponent<Tile>().Child.GetComponent<Unit>();
            if(u != null && u.OwnerID==0)
            {
                OnLoot(u);
            }
        };
    }

    public void RemoveLoot()
    {
        Destroy(LootObject);
        Destroy(this);
    }    
    
    public void OnLoot(Unit _u)
    {
        u = _u;
        LootChoice();
       
    }

    void BuffDmg(Unit _u)
    {
        (u.Actions.GetAction("Attack") as UnitAction_Attack).DMG.amount += 1;
    }
    
    void BuffWalkRange(Unit _u)
    {
        (u.Actions.GetAction("Move") as UnitAction_Move).MoveRange += 1;
    }

    void GetRest(Unit _u)
    {
        (u.Actions.GetAction("Rest") as UnitAction_Rest).Charges += 1;
    }

    void LootChoice()
    {
        UI_Choice.CreateUIChoice(new string[]
        {
            "+1 Damage", "+1 Move Range","+1 Rest"
        }, LootChoiceMade);
    }

    void LootChoiceMade(int i)
    {
        switch (i)
        {
            case 0:
                BuffDmg(u);
                break;
            case 1:
                BuffWalkRange(u);
                break;
            case 2:
                GetRest(u);
                break;
        }

        RemoveLoot();
    }


    

}
