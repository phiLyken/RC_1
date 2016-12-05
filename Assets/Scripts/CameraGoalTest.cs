using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraGoalTest : MonoBehaviour {

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           StrategyCamera.Instance.ActionPanToPos.GoToPos(transform.position, ChangeColor );
            transform.DORewind();
            transform.DOScale(0.6f, 0.2f).SetEase(Ease.OutCubic);
        }
    }

    void ChangeColor()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.25f, 0.2f).SetEase(Ease.OutCubic));
        seq.Append(transform.DOScale(0.5f, 0.5f).SetEase(Ease.InOutBounce));
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
