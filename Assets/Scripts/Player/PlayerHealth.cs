﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int startingHealth = 300;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    // PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake()
    {
        //Mendapatkan refernce komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        healthSlider = GameObject.FindGameObjectWithTag("PlayerHeartSlider").GetComponent<Slider>();
        damageImage = GameObject.FindGameObjectWithTag("PlayerDamageImage").GetComponent<Image>();

        // playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update()
    {
        //Jika terkena damaage
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //Fade out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //Set damage to false
        damaged = false;
    }

    //fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        if(!CheatManager.isNoDamage)
        {
            //mengurangi health
            currentHealth -= amount;

            //Merubah tampilan dari health slider
            healthSlider.value = currentHealth;
        }
        

        //Memainkan suara ketika terkena damage
        playerAudio.Play();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Heal(int amount)
    {
        // increment health until it reaches its startingHealth
        if (currentHealth + amount <= startingHealth)
        {
            currentHealth += amount;
            healthSlider.value = currentHealth;
        }
        else
        {
            currentHealth = startingHealth;
            healthSlider.value = currentHealth;
        }
    }


    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;
        Gameover.instance.ShowGameOverScreen();
        Gameover.isGameOver = true;
    }


    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }

    public void LoadData(GameData data)
    {
        this.currentHealth = data.health;
    }

    public void SaveData(ref GameData data)
    {
        data.health = this.currentHealth;
    }
}