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
        int collisionLayer = 1 << collision.collider.gameObject.layer;
        if (collisionLayer == environmentLayer)
        {
            Death();
        }
    }

    public void Death()
    {
        isDestroyed = true;
        destroyParticle.Play();
        Destroy(gameObject, 0.5f);
    }
}
