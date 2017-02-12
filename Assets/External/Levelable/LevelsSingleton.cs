using UnityEngine;
using System.Collections;

public class PlayerLevel : Levels {

    static PlayerLevel _instance;

    public static PlayerLevel Instance
    {
        get {
               return _instance == null ? makeInstance() : _instance;
        }
    }

    public PlayerLevel(LevelConfig conf) : base(conf)
    {
        _instance = this;
    }

    static string GetPathToConfig()
    {
        return "playerlevel_defaultbalancing";
    }

    static PlayerLevel makeInstance()
    {
      
        return new PlayerLevel(Resources.Load<ScriptableLevelConfig>(GetPathToConfig()).Config);
    }
}
