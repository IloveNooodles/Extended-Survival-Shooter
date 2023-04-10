using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHealth : EnemyHealth
{
    public int knockDownDuration = 20;
    bool isDead;
    float healthBarLength;

    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    Animator anim;
    WizardAttack wizardAttack;
    
    void Start(){
        Id = EnemyName.GetEnemyId(EnemyName.Wizard);

        //Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        wizardAttack = GetComponent<WizardAttack>();
        healthBarLength = healthBar.rectTransform.rect.width;
    }

    void Update()
    {
    }

    public override void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if (isDead)
            return;

        //play audio
        // enemyAudio.Play();

        //kurangi health
        currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    public override void Death()
    {
        //set isdead
        isDead = true;
        //reset attack
        wizardAttack.Reset();

        //trigger play animation Dead
        anim.SetTrigger("Dead");
        anim.SetBool("isDead", true);

        //Play Sound Dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        
        StartCoroutine(Resurect());
    }

    IEnumerator Resurect(){
        yield return new WaitForSeconds(knockDownDuration);
        isDead = false;
        anim.SetBool("isDead", false);
        currentHealth = startingHealth;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);
        wizardAttack.Reset();
    }
}
