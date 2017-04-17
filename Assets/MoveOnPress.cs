using UnityEngine;
using System.Collections;
using DG.Tweening;
public class MoveOnPress : MonoBehaviour {

    public Transform MoveObject;
    public Transform EndPos;
    public Transform StartPos;
    public float Speed;
    public float HoldTime;
    public Ease EaseCurve;
    public KeyCode InputKey;

    GameObject ui;

    Sequence tween;
    void Start()
    {
        MoveObject.gameObject.SetActive(false);
    }
    void Update()
    {

        if (Application.isEditor && Input.GetKeyDown(InputKey))
        {
            if (ui == null)
            {
                ui = GameObject.FindGameObjectWithTag("UI");
              
            }

            ui.SetActive(false);
            MoveObject.gameObject.SetActive(true);
            float time = (EndPos.position - StartPos.position).magnitude / Speed;

            tween.Rewind();
            tween = DOTween.Sequence();

            MoveObject.transform.position = StartPos.position;
            tween.Append(MoveObject.transform.DOMove(EndPos.position, time).SetEase(EaseCurve));
            tween.AppendInterval(HoldTime);
            tween.AppendCallback(() =>ui.SetActive(true));
            tween.AppendCallback(()=>MoveObject.gameObject.SetActive(false));

         

        }
    }
}
