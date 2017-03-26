using UnityEngine;
using System.Collections;

public class UnitAdrenalineRushParticleManager : UI_AdrenalineRushBase {

    public float ActiveParticleDelay;

    public AudioClip GainSound;

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
        SoundManager.PlaySFX(GainSound, this.transform);
        EnableRushParticles();
    }

    protected override void RushLoss()
    {
        DisableRushParticles();
    }

    public void EnableRushParticles()
    {
        RushGainParticle.OneShotParticle(transform.position);
        this.ExecuteDelayed(ActiveParticleDelay, () => active_particles.ToggleParticles(true));
    }

    public void DisableRushParticles()
    {
        this.ExecuteDelayed(0, () => active_particles.ToggleParticles(false));
        RushFadeParticle.OneShotParticle(transform.position);
    }



   
}
