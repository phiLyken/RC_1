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

           
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Sequence sq = DOTween.Sequence().Append(d(1))
            ;
            sq.AppendInterval(1);
            sq.AppendCallback(() => { MDebug.Log("1"); });
        
            sq.AppendCallback(() => { MDebug.Log("1"); });
            sq.Append(d(2));
            sq.Append(d(3));
            sq.AppendCallback(() => { MDebug.Log("1"); });
     //       sq.Play();

        }
    }

    Tween d(int id)
    {
        Sequence s = DOTween.Sequence();
       
        s.AppendInterval(1);
        s.AppendCallback(() => { MDebug.Log(" -"+id); });

        Sequence r = DOTween.Sequence();
        r.AppendInterval(5);
        r.AppendCallback(() => { MDebug.Log("     -" + id); });
        return s;
        ;
    }
}
