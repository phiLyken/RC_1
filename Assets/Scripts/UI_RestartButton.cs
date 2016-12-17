using UnityEngine;
using System.Collections;

public class UI_RestartButton : MonoBehaviour {

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
