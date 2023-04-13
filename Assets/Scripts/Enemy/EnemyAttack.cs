using System;
using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    private float petTimer;

    GameObject petInRange;

    void Awake()
    {
        //Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        //mendapatkan komponen player health
        playerHealth = player.GetComponent<PlayerHealth>();

        //mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        //Mendapatkan Enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }


    //Callback jika ada suatu object masuk kedalam trigger
    void OnTriggerEnter(Collider other)
    {
        //Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }

        if (other.gameObject == PetManager.currentPet)
        {
            petInRange = other.gameObject;
        }
    }

    //Callback jika ada object yang keluar dari trigger
    void OnTriggerExit(Collider other)
    {
        //Set player jika tidak dalam range
        if (other.gameObject == player)
        {
            playerInRange = false;
        }

        if (other.gameObject == PetManager.currentPet)
        {
            petInRange = null;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        petTimer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (petTimer >= timeBetweenAttacks && petInRange != null && enemyHealth.currentHealth > 0)
        {
           AttackPet();
        }

        //mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        //Reset timer
        timer = 0f;

        //Taking Damage
        if (playerInRange && playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void AttackPet()
    {
        petTimer = 0f;
        PetHealth petHealth = petInRange.GetComponent<PetHealth>();
        if (petHealth.currentHealth > 0)
        {
            petHealth.TakeDamage(attackDamage);
        }
    }
}