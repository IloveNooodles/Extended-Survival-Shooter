using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    private Slider healthSlider;
    // public AudioClip deathClip;


    Animator anim;
    // AudioSource petAudio;

    PlayerMovement petMovement;

    // PlayerShooting playerShooting;
    bool isDead;

    void Awake()
    {
        healthSlider = GameObject.Find("PetHeartSlider").GetComponent<Slider>();
         //Mendapatkan refernce komponen
        anim = GetComponent<Animator>();
        // playerAudio = GetComponent<AudioSource>();
        petMovement = GetComponent<PlayerMovement>();

        // playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        healthSlider.value = currentHealth;
        healthSlider.gameObject.SetActive(true);
    }


    //fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        // damaged = true;

        //mengurangi health
        currentHealth -= amount;

        //Merubah tampilan dari health slider
        healthSlider.value = currentHealth;

        //Memainkan suara ketika terkena damage
        // petAudio.Play();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 0 dan belu mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    

    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        // petAudio.clip = deathClip;
        // petAudio.Play();

        //mematikan script player movement
        petMovement.enabled = false;
        

        // playerShooting.enabled = false;
        
        
        healthSlider.gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        if(healthSlider == null) return;
        healthSlider.gameObject.SetActive(false);
    }
}