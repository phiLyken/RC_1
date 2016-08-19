using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RemoveParticles : MonoBehaviour {

    List<ParticleSystem> systems;

    void Awake()
    {
        systems = new List<ParticleSystem>();
        systems.Add(GetComponent<ParticleSystem>());
        systems.AddRange(GetComponentsInChildren<ParticleSystem>().ToList());

    }

    void Update()
    {
        if (gameObject.activeSelf)
        {

            foreach (ParticleSystem sys in systems)
            {

                if (sys.IsAlive())
                    return;
            }

            Destroy(this.gameObject);
            gameObject.SetActive(false);
        }

    }
    
}
