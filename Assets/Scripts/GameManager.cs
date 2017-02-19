﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour, IInit {

    public event Action<RegionConfigDataBase> OnRegionSet;

    public RegionConfigDataBase DefaultRegionConfig;

    private RegionConfigDataBase _choosenregion;
    public RegionConfigDataBase ChoosenRegionConfig
    {
        get
        {
            return _choosenregion;
        }
        set
        {
            _choosenregion = value;
            OnRegionSet.AttemptCall(_choosenregion);
        }
    }

    public void Init()
    {
        if(ChoosenRegionConfig == null)
        {
            SetRegion(DefaultRegionConfig);
        }
    }
    public static void SetRegion(RegionConfigDataBase conf)
    {
     //   Instance.ChoosenRegion = conf;
    }

 
    public static void MissionEnded()
    {

       
    }

    public static void GoToSquad()
    {

        SceneManager.LoadScene("_squad_selection");
    }
    public static void StartMission()
    {
        if(Instance.ChoosenRegionConfig == null )
        {
            Debug.Log("^game Region selected");
            return;
        }

        if(SquadManager.Instance.selected_units.IsNullOrEmpty() && !Instance.ChoosenRegionConfig.IsTutorial)
        {
            Debug.Log("^game No Units Selected");
            return;
        }

        SceneManager.LoadScene("_engine_test_game");

    }

    public static GameManager Instance
    {
        get { return _instance == null ?  M_Extensions.MakeMonoSingleton<GameManager>(out _instance) : _instance; }
    }

    static GameManager _instance;

}
