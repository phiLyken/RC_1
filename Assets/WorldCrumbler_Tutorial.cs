﻿using UnityEngine;
using System.Collections;

public class WorldCrumbler_Tutorial : WorldCrumbler {
    
    bool first = true;
    int _cachedCrumbleRange;
    void Start()
    {
        _cachedCrumbleRange = Constants.CrumbleRange;
        Constants.CrumbleRange = 15;
        MissionSystem.OnCompleteMission += mission =>
        {
            if (mission.GetSaveID() == "loot")
            {
                foreach (Tile t in FindObjectsOfType<Tile>())
                {
                    OnCrumble += t.OnCrumbleTurn;
                }
                Init(TurnSystem.Instance);
            }
        };
    }
    protected override int GetCrumbleCount()
    {
        return first ? 150 : base.GetCrumbleCount();
    }
    public override void StartTurn()
    {
        hasCrumbled = false;
        if (first)
        {
         
            StartCoroutine(FirstCrumbleSequence());
        } else
        {
            base.StartTurn();
        }
    }

    IEnumerator FirstCrumbleSequence()
    {
        ToastNotification.SetToastMessage1("HQ: Commander! We've received reports of an explosion in one of our mines.");
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine( RC_Camera.Instance.ActionPanToPos.GoToPos(GetCameraPosition()));
        yield return new WaitForSeconds(4f);   
      

        CrumbleTurn();
        hasCrumbled = false;
        ToastNotification.SetToastMessage1("WHAT?! The planets' surface is collapsing.");
        yield return new WaitForSeconds(5f);

        ToastNotification.SetToastMessage1("Commander Evac! Immidiately!");
        yield return new WaitForSeconds(3f);
        Constants.CrumbleRange = _cachedCrumbleRange;
        hasCrumbled = true;
        first = false;

    }

    protected override void PostMoveTiles()
    {
        if (first)
        { 
            hasCrumbled = false;
        } else
        hasCrumbled = !first;
    }


}