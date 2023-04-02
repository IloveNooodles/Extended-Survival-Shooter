using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenCars : MonoBehaviour, IEnvironment
{
    public ParticleSystem destroyedParticles;
    public Gate gate;
    public GameObject healthBarCanvas;
    public AudioClip destroyedClip;
    EnvironmentHealth environmentHealth;
    AudioSource audioSource;
    GameObject cars;
    Collider carCollider;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cars = this.transform.GetChild(0).gameObject;
        environmentHealth = GetComponent<EnvironmentHealth>();
        carCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if(gate.isDestroyed){
            environmentHealth.enabled = true;
            healthBarCanvas.SetActive(true);
        }
    }

    public void Death(){
        audioSource.clip = destroyedClip;
        audioSource.Play();
        destroyedParticles.Play();
        carCollider.enabled = false;
        Destroy(cars, 1f);
        Destroy(this.gameObject, 4f);
    }
}
