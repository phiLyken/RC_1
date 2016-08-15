using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ShootBullet : MonoBehaviour
{
    public int bullet_count;
    public float intervall;
    public BulletConfig Bullet;
    public GameObject ShootEffectOnWeapon;
    public GameObject HitEffect;

    public Sequence Shoot(Transform target)
    {
        return ShootBullets(target);
    }

    public Sequence ShootBullets(Transform target)
    {

        Sequence seq = DOTween.Sequence();
        Sequence ret = DOTween.Sequence().AppendInterval(intervall * bullet_count);

        for (int i = 0; i < bullet_count; i++)
        {
            if (i == bullet_count - 1)
            {
                ret.Append(ShootOneBullet(target));
                break;

            }
            seq.AppendCallback ( () => ShootOneBullet(target));
            seq.AppendInterval(intervall);           
        }

     
        return ret;
    }

    Sequence ShootOneBullet(Transform target)
    {
        return BulletEmitter.SpawnBullet(Bullet, gameObject.transform, target);
        
    }
}