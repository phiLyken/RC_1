using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    
    static int to_load = -1;

    public CanvasGroup Splash;
    public CanvasGroup Loading;
    bool showsplash;

    public static void LoadScene(int index)
    {
        to_load = index;
        SceneManager.LoadScene(0);
    }

    void Awake()
    {
        if(to_load < 0 )
        {
            showsplash = true;     
        }

        StartCoroutine(LoadNewScene());
    }
    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        Splash.gameObject.SetActive(showsplash);
         
        if (showsplash)
        {
            Loading.gameObject.SetActive(false);
            showsplash = false;
            to_load = 1;
            yield return  StartCoroutine(M_Extensions.YieldT(f => Splash.alpha = f, 0.5f));
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(M_Extensions.YieldT(f => Splash.alpha = 1-f, 0.5f));
        } else
        {
            yield return new WaitForSeconds(0.5f);
        }

        Loading.gameObject.SetActive(true);
        Loading.alpha = 0;
        yield return StartCoroutine(M_Extensions.YieldT(f => Loading.alpha = f, 0.25f));
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(to_load, LoadSceneMode.Single);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            Debug.Log(async.progress+ " "+Time.time);
            yield return null;
        }

       
        yield return StartCoroutine(M_Extensions.YieldT(f => Loading.alpha = 1- f, 1f));
        yield return new WaitForSeconds(0.5f);
       
       
        
    }


}
