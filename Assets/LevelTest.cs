using UnityEngine;
using System.Collections;

public class LevelTest : MonoBehaviour {

    public LevelConfig LevelTestConfig;

    Levels m_levels;

    void Start()
    {
        m_levels = new Levels(LevelTestConfig);
        m_levels.OnLevelUp += onlvl;
        m_levels.OnProgress += onprogress;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_levels.AddProgress(20);
        }
    }

    void onlvl(int newlevel)
    {
        Debug.Log("LV "+newlevel+" %"+m_levels.GetProgressInLevel());
    }

    void onprogress(int progress)
    {
        Debug.Log("PR " + progress+"  %"+m_levels.GetProgressInLevel());
    }
}
