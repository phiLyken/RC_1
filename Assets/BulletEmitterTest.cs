using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletEmitterTest : MonoBehaviour {

    public GameObject Target;
    public ShootBullet Shoot;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //   BulletEmitter.SpawnBullet(Config, gameObject.transform, Target.transform);
            Sequence sq = DG.Tweening.DOTween.Sequence();

            sq.Append(Shoot.Shoot(Target.transform));

            sq.AppendCallback(() => { Debug.Log("Sequence over"); });
        }
    }
    

}
