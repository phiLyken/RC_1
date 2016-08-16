using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ShootBullet : MonoBehaviour
{
    public int bullet_count;
    public float intervall;
    public BulletConfig Bullet;
    public EffectSpawner ShootEffectOnWeapon;
    public EffectSpawner HitEffect;

    
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
        if (ShootEffectOnWeapon != null)
            ShootEffectOnWeapon.Init(transform.gameObject);

        return BulletEmitter.SpawnBullet(Bullet, gameObject.transform, target);
        
    }
}

[System.Serializable]
public class EffectSpawner{

    public AudioClip Sound;
    public ParticleSystem Particle;
    public LightFlash Flash;

    public void Init(GameObject target)
    {
        if(Sound != null)
        {

        }

        if(Particle != null)
        {
            GameObject go = GameObject.Instantiate(Particle, target.transform.position, target.transform.rotation) as GameObject;
  

        }

        if(Flash != null)
        {
            Flash.Init(target);
        }
    }
}

[System.Serializable]
public class LightFlash
{
    public float base_intensity;
    public float radius;
    public Color startColor;
    public Color endColor;
    public AnimationCurve intensity;
    public float duration;

    public static void Init(LightFlash flash, GameObject target)
    {
        GameObject new_go = new GameObject("_flash");
        new_go.transform.position = target.transform.position;
        
        Light l =  new_go.AddComponent<Light>();
        l.range = flash.radius;
        l.intensity = flash.base_intensity;

        Sequence FlashSequence = DOTween.Sequence();
        FlashSequence.Append(    DOTween.To(() => l.intensity, _l => l.intensity = _l,0, flash.duration).SetEase(flash.intensity));
        FlashSequence.AppendCallback(() => GameObject.Destroy(new_go));

        l.color = flash.startColor;
        l.DOColor(flash.endColor, flash.duration);
      
    }

    public void Init(GameObject target)
    {
        Init(this, target);
    }
   
}