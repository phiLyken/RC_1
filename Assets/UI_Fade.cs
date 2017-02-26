using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour {

    public CanvasGroup To_Fade;

    void Start()
    {
        To_Fade.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(M_Extensions.YieldT(SetAlpha, 0.75f));
    }

    void SetAlpha(float f)
    {
        To_Fade.alpha = 1 - f;
    }
}
