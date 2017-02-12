using UnityEngine;
using System.Collections;

public class TutorialLock : MonoBehaviour {

    public GameObject Target;

    void Awake()
    {

        Target.SetActive(PlayerPrefs.GetInt(Constants.TUTORIAL_SAVE_ID) == 0);
        
    }
}
