using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class TrackingTest_Mono : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Reference the Collections Generic namespace
        TrackingManager.TrackingCall_LevelComplete(
           Resources.Load<ScriptableRegionDataBaseConfigs>("Regions/RegionConfigs/selectablemissions_defaultbalancing").RegionConfigs.GetRandom(),
          new MissionOutcome(4, 2, 100, 1, 4, 0.5f, 3, 5), 10);

      
        int totalPotions = 5;
        int totalCoins = 100;
        string weaponID = "Weapon_102";
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
            { "potions", totalPotions },
            { "coins", totalCoins },
            { "activeWeapon", weaponID }
          });
        
   
        Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);

        Gender gender = Gender.Female;
        Analytics.SetUserGender(gender);

        int birthYear = 2014;
        Analytics.SetUserBirthYear(birthYear);
    
    }
	
	// 
}
