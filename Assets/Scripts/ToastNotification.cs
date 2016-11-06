using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToastNotification : MonoBehaviour {


    public Text TF;
    public Image BG;

   static ToastNotification Instance;

    void Awake()
    {
        Instance = this;
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

        StartCoroutine(ShowToast());
    }
    IEnumerator ShowToast()
    {
        yield return new WaitForSeconds(0.15f);
        ToggleActive(true);
        yield return new WaitForSeconds(2f);
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
    static void GetInstance()
    {
        Instance = FindObjectOfType<ToastNotification>();
        
        if(Instance == null)
        {
            Debug.LogWarning("Toast Notifaction not found");
        }
        
    }
}
