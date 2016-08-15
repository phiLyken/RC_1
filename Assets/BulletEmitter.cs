using UnityEngine;
using System.Collections;
using DG.Tweening;


public class BulletEmitter   {


    public static Sequence SpawnBullet(BulletConfig Bullet, Transform start, Transform target)
    {
        Debug.Log("spawn");
        float distance = (start.position - target.position).magnitude;
        float time = distance / Bullet.speed;
        Vector3 offset_target = Random.insideUnitSphere * 1;
        Transform new_target = new GameObject().transform;
        MyMath.CopyTransform(target, new_target);

        new_target.SetParent(target);


        if(offset_target.magnitude > 0)
        {
            new_target.localPosition += offset_target;
        }


        GameObject bullet = GameObject.Instantiate(Bullet.projectile, start.position, start.rotation) as GameObject;

        //just to be safe we run an own sequence that is just based on time (and will be hooked to game logic)
        Sequence delayed_call_back = DOTween.Sequence();
        delayed_call_back.AppendInterval(time);
      
        Sequence emit = DOTween.Sequence();
        emit.Append(    bullet.transform.DOMove(new_target.position, time).SetEase(Bullet.Animation_Curve));
        emit.AppendCallback(() =>
          {
              bullet.transform.GetComponentInChildren<ParticleSystem>().StopAndKill();
              bullet.transform.DetachChildren();
              GameObject.Destroy(new_target.gameObject);
              GameObject.Destroy(bullet);
          });
        return delayed_call_back;

    }
}



[System.Serializable]
public class BulletConfig
{
    public GameObject projectile;
    public Ease Animation_Curve;
    public float speed;
    public float randomOffset;
}

[System.Serializable]
public class ShootEffects
{

}

