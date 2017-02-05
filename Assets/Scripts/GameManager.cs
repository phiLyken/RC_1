using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public RegionConfigDataBase DefaultRegion;

    public static RegionConfigDataBase ChoosenRegion;

    void Awake()
    {
        SetRegion(DefaultRegion);
    }
    public static void SetRegion(RegionConfigDataBase conf)
    {
        ChoosenRegion = conf;
    }

    public static void MissionEnded()
    {
        if(SquadManager.Instance.evacuated.Count > 0)
        {
            PlayerInventory.Instance.GetItem(ItemTypes.dust).SaveValue();
        }
        SceneManager.LoadScene("_squad_selection");
    }

    public static void StartMission()
    {
        if(ChoosenRegion == null || SquadManager.Instance.selected_units.IsNullOrEmpty())
        {
            Debug.Log("^game No Region or selected units");
            return;
        }

        SceneManager.LoadScene("_engine_test_game");
    }

}
