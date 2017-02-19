using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ToastNotification : MonoBehaviour, IInit {

    public Vector3 Position;
    public Text TF;
    public Image BG;

    static ToastNotification _instance;
    static ToastNotification Instance
    {
        get { return _instance == null ? M_Extensions.MakeMonoSingleton(out _instance, Resources.Load("UI/ui_toastnotification") as GameObject) : _instance; }  
    }
   
    void Awake()
    {       
        ToggleActive(false);
    }
    
    void ToggleActive(bool b)
    {
        TF.gameObject.SetActive(b);
        BG.gameObject.SetActive(b);
    }

    protected void ShowToastMessage( string text, Color bgcolor, Color textcolor)
    {
        StopToast();

        TF.text = text;
        BG.color = bgcolor;
        TF.color = textcolor;

        StartCoroutine(ShowToast(2+ text.Length * 0.05f));
    }
    IEnumerator ShowToast(float duration)
    {
        yield return new WaitForSeconds(0.15f);
        ToggleActive(true);
        yield return new WaitForSeconds(duration);
        ToggleActive(false);
    }

    public static void SetToastMessage1(string text)
    {
        SetToastMessageInInstance(text, new Color(0.1f, 0.1f,0.1f,1) , Color.white);
    }
    public static void SetToastMessage2(string text)
    {
        SetToastMessageInInstance(text, new Color(0.6f, 0.1f, 0.1f, 1), Color.white);
    }

    protected static void SetToastMessageInInstance(string text, Color bgcolor, Color textcolor)
    {
        if(Instance != null)
            Instance.ShowToastMessage(text, bgcolor, textcolor);      
    }

    public static void StopToast()
    {
        if (Instance == null) return;
        Instance.StopAllCoroutines();
        Instance.ToggleActive(false);
    }



    void IInit.Init()
    {
        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        this.transform.SetParent(ui.transform);
       ( this.transform as RectTransform).anchoredPosition = Position;
    }
}
