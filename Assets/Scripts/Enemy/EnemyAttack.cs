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
    bool playerInRange, petInRange;
    float timer;


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
        
        //Set pet in range
        if (other.gameObject.tag.Equals("Pet") && other.isTrigger == false)
        {
            petInRange = true;
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
        
        //Set pet jika tidak dalam range
        if (other.gameObject.tag.Equals("Pet"))
        {
            petInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && (playerInRange || petInRange) && enemyHealth.currentHealth > 0)
        {
            Attack();
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
        
        GameObject pet = GameObject.FindGameObjectWithTag("Pet");
        if (petInRange && pet != null)
        {
            PetHealth petHealth = pet.GetComponent<PetHealth>();
            if (petHealth.currentHealth > 0)
            {
                petHealth.TakeDamage(attackDamage);
            }
        }
    }
}