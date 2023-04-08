using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public int goldValue = 1;
    public AudioClip deathClip;
    public Image healthBar;
    float healthBarLength;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        //Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Set current health
        currentHealth = startingHealth;
        healthBarLength = healthBar.rectTransform.rect.width;
    }


    void Update()
    {
        //Check jika sinking
        if (isSinking)
        {
            //memindahkan object kebawah
            transform.Translate(-Vector3.up * (sinkSpeed * Time.deltaTime));
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if (isDead)
            return;

        //play audio
        enemyAudio.Play();

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

    void Death()
    {
        //set isdead
        isDead = true;

        //SetCapcollider ke trigger
        capsuleCollider.isTrigger = true;

        //trigger play animation Dead
        anim.SetTrigger("Dead");

        //Play Sound Dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        try
        {
            FPSEnemyManager.numberOfEnemy--;
        }
        catch { }
    }

    /* Trigger the animation for the death*/
    public void StartSinking()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        
        //Set rigisbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        GoldManager.Gold += goldValue;
        Destroy(gameObject, 2f);
    }
}