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
    public AudioClip deathClip;


    Animator anim;
    AudioSource petAudio;

    PetMovement petMovement;

    private PetCast petCast;
    bool isDead;

    void Awake()
    {
        healthSlider = GameObject.FindWithTag("PetHeartSlider").GetComponent<Slider>();
         //Mendapatkan refernce komponen
        anim = GetComponent<Animator>();
        petAudio = GetComponent<AudioSource>();
        petMovement = GetComponent<PetMovement>();

        // playerShooting = GetComponentInChildren <PlayerShooting> ();
        petCast = GetComponent<PetCast>(); 
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
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
        if(!CheatManager.isFullHPPet){
            currentHealth -= amount;

            //Merubah tampilan dari health slider
            healthSlider.value = currentHealth;
        }


        //Memainkan suara ketika terkena damage
        petAudio.Play();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 0 dan belu mati
        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Death());
        }
    }

    public void killPet()
    {
        currentHealth -= 0;
        healthSlider.value = currentHealth;
        petAudio.Play();
        StartCoroutine(Death());
    }
    

    IEnumerator Death()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        petAudio.clip = deathClip;
        petAudio.Play();
        
        // get current animation clip
        AnimationClip clip = anim.runtimeAnimatorController.animationClips[0];
        float duration = Mathf.Max(clip.length, deathClip.length);
        
        //mematikan script player movement
        petMovement.enabled = false;
        
        healthSlider.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        if(healthSlider == null) return;
        healthSlider.gameObject.SetActive(false);
    }
}