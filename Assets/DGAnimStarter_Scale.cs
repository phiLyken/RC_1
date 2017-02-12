using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DGAnimStarter_Scale : MonoBehaviour {


    public float Scale;
    public float StartScaleMod;

    public float Duration;
    public Ease Ease;

    Vector2 startScale;

    void Awake()
    {
        startScale = transform.localScale * StartScaleMod;
    }

    void OnEnable()
    {
        Sequence anim = DOTween.Sequence();
        transform.DORewind();
        transform.localScale = startScale;
        anim.Append(transform.DOScale(Scale, Duration).SetEase(Ease));
        anim.Play();
    }
}
