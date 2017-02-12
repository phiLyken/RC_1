using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UI_SquadSlotController : MonoBehaviour {
    public Text SelectedNumberTF;
    public GameObject target;

    List<UI_SquadSlot> Slots;

    void Awake()
    {
        MakeSlots();
        SquadManager.Instance.OnSelectedUpdate += OnUpdate;        

        OnUpdate(SquadManager.Instance.selected_units); 
        
        UpdateTF();
        
    }

    void OnDestroy()
    {
        SquadManager.Instance.OnSelectedUpdate -= OnUpdate;
    }
    int getHighestSlotCount()
    {
       return SquadManager.Instance.GetSquadSizeUnlockes().Last().Item.Size;
    }
    void MakeSlots()
    {
        int count = getHighestSlotCount();

        Slots = new List<UI_SquadSlot>();
        List<Unlockable<SquadSizeConfig>> sizes = new List<Unlockable<SquadSizeConfig>>( SquadManager.Instance.GetSquadSizeUnlockes());
        Unlockable<SquadSizeConfig> current = sizes[0];
       

        for (int i = 0; i < count; i++)
        {          
            if(i == current.Item.Size)
            {
                sizes.Remove(current);

                if(sizes.Count>0)
                    current = sizes[0];
            }
            

            Slots.Add(UI_SquadSlot.MakeSlot(current, target.transform));
        }
    }
    void OnUpdate(List<TieredUnit> selected)
    {
        for(int i = 0; i < getHighestSlotCount(); i++)
        {
            ScriptableUnitConfig unit = null;

            if(i < selected.Count)
            {
                unit = TieredUnit.Unlocks(selected[i].Tiers, PlayerLevel.Instance).GetHighestUnlocked().Config;
            }
            Slots[i].SetUnit(unit);
        }
        UpdateTF();
    }

    void UpdateTF()
    {
        string text = SquadManager.Instance.selected_units.Count +"/"+SquadManager.Instance.GetMaxSquadsize();
        SelectedNumberTF.text = text;
    }
    void OnRemove(UI_SquadSlot view)
    {

    }
}
