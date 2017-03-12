using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KongregateAPIBehaviour : MonoBehaviour
{
    private static KongregateAPIBehaviour instance;


    string username;
    string gameAuthToken;
    int userId;

    void Start()
    {
       
        if (instance == null)
        {
            if (Application.isEditor)
            {
                OnKongregateUserInfo("123|TEST|AUTH");
            }
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Object.DontDestroyOnLoad(gameObject);
        gameObject.name = "KongregateAPI";

        Application.ExternalEval(
          @"if(typeof(kongregateUnitySupport) != 'undefined'){
        kongregateUnitySupport.initAPI('KongregateAPI', 'OnKongregateAPILoaded');
      };"
        );


       

    }

    public void OnKongregateAPILoaded(string userInfoString)
    {
        OnKongregateUserInfo(userInfoString);
    }

    public void OnKongregateUserInfo(string userInfoString)
    {
        var info = userInfoString.Split('|');
        userId = System.Convert.ToInt32(info[0]);
        username = info[1];
        gameAuthToken = info[2];
        Debug.Log("Kongregate User Info: " + username + ", userId: " + userId);

        Unit.OnUnitKilled += OnKill;
        PlayerLevel.Instance.OnLevelUp += OnLevelUp;
        MissionOutcome.OnMissionOutcomeSet += OnOutcome;
        Application.ExternalCall("kongregate.stats.submit", "initialized", 1);
    }
    
    void OnOutcome(MissionOutcome outcome)
    {
       

       var Regions = new List<RegionConfigDataBase>(Resources.Load<ScriptableRegionDataBaseConfigs>("Regions/RegionConfigs/selectablemissions_defaultbalancing").RegionConfigs);

        int saved = 0;
        foreach(var region in Regions)
        {
            saved += region.IsCompleteInSave() ? 1 : 0;
        }

        Application.ExternalCall("kongregate.stats.submit", "SECURED SUPPLIES", outcome.SuppliesGainedFinal);
        Application.ExternalCall("kongregate.stats.submit", "MISSIONS CLEARED", saved);
        if(saved >= Regions.Count)
        {
            Application.ExternalCall("kongregate.stats.submit", "GameComplete 1", 1);
        }
    }

    void OnLevelUp(int obj)
    {
        Application.ExternalCall("kongregate.stats.submit", "PLAYER LEVEL", obj);
    }

 
    void OnKill(Unit u)
    {
        if(u.OwnerID == 1)
        {
            Application.ExternalCall("kongregate.stats.submit", "PRISONERS PACIFIED", 1);
        }
    }
}
