using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Level : MonoBehaviour {

    public UI_Bar Bar;
    public Text CurrentProgressTF;
    public Text CurrentLevelTF;

    Levels m_levels;

    protected void Init(Levels levels)
    {
        m_levels = levels;
        levels.OnProgress += total => UpdateProgress(levels.GetProgressInLevel());
        UpdateProgress(levels.GetProgress()); 
    }

    void UpdateProgress(float new_progress)
    {
        if (this ==  null)
        {
            return;
        }
        Bar.SetProgress(m_levels.GetProgressInLevel());
        CurrentProgressTF.text = m_levels.GetProgressInLevelAbsolute().ToString() + "/" + m_levels.GetRequiredforNextLevel();
        CurrentLevelTF.text = m_levels.GetCurrentLevel().ToString();
    }
	    
}
