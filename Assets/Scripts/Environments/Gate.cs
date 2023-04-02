using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IEnvironment
{
    public LayerMask environmentLayer;
    public ParticleSystem destroyParticle;
    public bool isDestroyed = false;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Clock")
        {
            Death();
        }
    }

    public void Death()
    {
        isDestroyed = true;
        try
        {
            destroyParticle.Play();
        }
        catch
        {

        }
        Destroy(gameObject, 0.5f);
    }
}
