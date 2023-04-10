using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isPreparingAttack = false;
    public bool isFireballBeingShooted = false;
    public float attackCooldown = 3f;
    public int attackDamage = 5;
    public AudioClip fireballSound;
    public int attackingRange = 50;
    public float projectileSpeed = 0.5f;
    float timer;
    float attackTimer;


    GameObject fireball;
    GameObject player;
    PlayerHealth playerHealth;
    Vector3 fireballTarget;
    EnemyHealth enemyHealth;
    Animator anim;
    AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireball = gameObject.transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();

        //Set attack cooldown
        attackCooldown = attackCooldown + 7;
    }

    void Update()
    {
        //Check if player is dead
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        //Check if enemy is dead
        if (enemyHealth.currentHealth <= 0)
        {
            return;
        }

        timer += Time.deltaTime;

        //Check if player is in range
        if(Vector3.Distance(transform.position, player.transform.position) <= attackingRange){
            //Check if enemy is not attacking
            if(!isAttacking){
                //Look at player
                if(!isFireballBeingShooted){
                    transform.LookAt(player.transform);
                }
            }

            //Check if attack is not on cooldown
            if(timer >= attackCooldown && !isAttacking && !isPreparingAttack){
                //Reset Timer
                timer = 0;
                attackTimer = 0;

                //Reset Fireball Value
                isFireballBeingShooted = false;
                fireball.transform.localPosition = new Vector3(0, 0.5f, 0.5f);
                fireball.transform.localScale = new Vector3(0, 0, 0);
                fireball.SetActive(true);

                //Set is preparing attack
                isPreparingAttack = true;

                //Set animation
                anim.SetBool("Attack", true);

                //Play fireball sound
                audioSource.volume = 0.2f;
                audioSource.PlayOneShot(fireballSound);
            }
        }
    }

    void FixedUpdate(){
        //Check player dead
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        //Check enemy dead
        if (enemyHealth.currentHealth <= 0)
        {
            return;
        }

        //Check if preparing attack
        if(isPreparingAttack){
            attackTimer += Time.deltaTime;
            
            //Scale up the fireball
            fireball.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            //If fireball is big enough
            if(attackTimer >= 4.5f){
                //Set is preparing attack to false
                isPreparingAttack = false;

                //Set is attacking to true
                isAttacking = true;

                //Set fireball active to true
                fireballTarget = gameObject.transform.forward;
                isFireballBeingShooted = true;
            } 
        }

        if(isAttacking){
            attackTimer += Time.deltaTime;

            if(attackTimer >= 7){
                //Set is attacking to false
                isAttacking = false;

                //Reset timer
                attackTimer = 0;
            }
        }

        if(isFireballBeingShooted){
            fireball.transform.position += fireballTarget * projectileSpeed;
        }
    }

    public void Reset(){
        //reset timer
        timer = 0;
        attackTimer = 0;

        //Reset Fireball Value
        isFireballBeingShooted = false;
        fireball.transform.localPosition = new Vector3(0, 0.5f, 0.5f);
        fireball.transform.localScale = new Vector3(0, 0, 0);
        fireball.SetActive(false);

        //Set is preparing attack
        isPreparingAttack = false;

        //Set is attacking
        isAttacking = false;
    }
}
