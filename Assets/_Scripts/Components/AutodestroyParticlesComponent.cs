using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestroyParticlesComponent : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    this.selfParticle = this.GetComponent<ParticleSystem>();
	    deactivateMethod = () => Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.selfParticle)
        {
            if (!this.selfParticle.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

    public AutodestroyParticlesComponent SetDeactivateMethod(Action newDeactivateMethod)
    {
        deactivateMethod = newDeactivateMethod;
        return this;
    }

    private ParticleSystem selfParticle;
    private Action deactivateMethod;
}
