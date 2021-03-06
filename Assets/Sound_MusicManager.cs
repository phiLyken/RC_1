﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class Sound_MusicManager : Sound_Singleton {

    public AudioClip WinMusic;
    public AudioClip FailedMusic;


    string current;

    protected override void PlaySoundForScene(string current_scene)
    {
 

        current = current_scene;

        GetSource().loop = true;

        if(current_scene == "_engine_test_game")
        {
            GetSource().Stop();
            Unit.OnUnitKilled += OnUnitCountUpdated;
            Unit.OnIdentifiedGlobal += OnUnitCountUpdated;
            GameEndListener.OnMissionEnded += GameEndListener_OnMissionEnded;
            Play(GameManager.Instance.ChoosenRegionConfig.DefaultMusic);

        } else
        {
           base.PlaySoundForScene(current_scene);
        }
    }


    private void GameEndListener_OnMissionEnded()
    {
        if (current == "_engine_test_game")
        {
            Unit.OnUnitKilled -= OnUnitCountUpdated;
            Unit.OnIdentifiedGlobal -= OnUnitCountUpdated;


        }

        GameEndListener.OnMissionEnded -= GameEndListener_OnMissionEnded;
        GetSource().loop = false;
        Play(MissionOutcome.LastOutcome.SquadUnitsEvaced > 0 ? WinMusic : FailedMusic);
     
    }

    void OnUnitCountUpdated(Unit u)
    {
      
        if(Unit.GetAllUnitsOfOwner(1,true).Count( unit => unit.IsIdentified) > 0 && GameManager.Instance.ChoosenRegionConfig.ActionMusic1 != null)
        {

            Play(GameManager.Instance.ChoosenRegionConfig.ActionMusic1);
        } else if(GameManager.Instance.ChoosenRegionConfig.DefaultMusic != null)
        {
            Play(GameManager.Instance.ChoosenRegionConfig.DefaultMusic);
        }
    }
 


}
