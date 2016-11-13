using UnityEngine;
using System.Collections;

public class UnitAdrenalineRushParticleManager : UI_AdrenalineRushBase {

    public float ActiveParticleDelay;
    

    public GameObject RushGainParticle;
    public GameObject RushActiveParticle;
    public GameObject RushFadeParticle;

    GameObject active_particles;

    void Awake()
    {
        active_particles = RushActiveParticle.Instantiate(transform, true);
        active_particles.ToggleParticles(false);
    }

    protected override void RushGain()
    {
        Debug.Log("Rush Gain");
     
       RushGainParticle.OneShotParticle(transform.position);
        
        this.ExecuteDelayed(ActiveParticleDelay, () => active_particles.ToggleParticles(true));
    }


    protected override void RushLoss()
    {
        Debug.Log("Rush loss");

        this.ExecuteDelayed(0, () => active_particles.ToggleParticles(false));
       
        RushFadeParticle.OneShotParticle(transform.position);
    }

   
}
