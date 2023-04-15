using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    public GameObject summoningMagic;
    private void Awake()
    {
        GameObject magic = Instantiate(
            summoningMagic,
            transform.position,
            transform.rotation
        );
        
        magic.transform.parent = transform;

        ParticleSystem particle = magic.GetComponent<ParticleSystem>();
        // get duration
        float totalDuration = particle.main.duration;
        
        
        Destroy(magic, totalDuration);
    }

    
}
