using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sound_MusicManager : Sound_Singleton {

    public AudioClip WinMusic;
    public AudioClip FailedMusic;


    string current;

    protected override void PlaySoundForScene(string current_scene)
    {
        current = current_scene;

       

        if(current_scene == "_engine_test_game")
        {
            GetSource().Stop();
            Unit.OnUnitKilled += OnUnitKilled;
            GameEndListener.OnMissionEnded += GameEndListener_OnMissionEnded;

        } else
        {
           base.PlaySoundForScene(current_scene);
        }
    }


    private void GameEndListener_OnMissionEnded()
    {
     
        Play(MissionOutcome.LastOutcome.SquadUnitsEvaced > 0 ? WinMusic : FailedMusic);
     
    }

    void OnUnitKilled(Unit u)
    {
        if(Unit.GetAllUnitsOfOwner(1,true).Count > 0)
        {
            MDebug.Log("Play Music 1");
        } else
        {
            MDebug.Log("Play Music 2");
        }
    }

    void OnDisable()
    {
        if (current == "_engine_test_game")
        {
            Unit.OnUnitKilled -= OnUnitKilled;
            GameEndListener.OnMissionEnded -= GameEndListener_OnMissionEnded;
        }
    }

}
