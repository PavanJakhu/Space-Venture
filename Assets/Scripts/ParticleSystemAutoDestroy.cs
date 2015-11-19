using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private List<ParticleSystem> childParticleSystems = new List<ParticleSystem>();
    private ParticleSystem ps;

    public void Start()
    {
        foreach (Transform child in transform)
        {
            childParticleSystems.Add(child.GetComponent<ParticleSystem>());
        }
    }

    public void Update()
    {
        bool oneAlive = false;
        foreach (ParticleSystem systems in childParticleSystems)
        {
            if (systems.IsAlive())
            {
                oneAlive = true;
            }
        }

        if (!oneAlive)
        {
            Destroy(gameObject);
        }
    }
}