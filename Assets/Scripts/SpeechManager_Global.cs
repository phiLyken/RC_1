﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpeechManager_Global : MonoBehaviour {
    
    void Start()
    {
        Unit.OnUnitKilled += Killed;

        if(WorldCrumbler.Instance != null)
             WorldCrumbler.Instance.OnCrumble += Crumble;

   
    }

    void Killed(Unit u)
    {
        getSpeeches(u.OwnerID == 1 ? 0 : 1, 1, 1, null).ForEach(s => s.ShowFoeDiedSpeech());
        getSpeeches(u.OwnerID , 1, 2, u).ForEach(s => s.ShowFriendDiedSpeech());
    }

    void Crumble(int c)
    {
        getSpeeches(-1,0,2,null).ForEach(s => s.ShowCrumbleSpeech());

    }

 



    List<SpeechManager_Unit> getSpeeches(int owner, int min, int max, Unit exclude){
        List<Unit> units = Unit.GetAllUnitsOfOwner(owner, true);

        int count = Mathf.Min(Random.Range(min, max+1), units.Count);

        List<Unit> picked = M_Math.GetRandomObjects(new List<Unit>(units).Where(u=> u != exclude).ToList(), count);

        return picked.Select(p => p.GetComponent<SpeechManager_Unit>()).ToList();
    }
}
