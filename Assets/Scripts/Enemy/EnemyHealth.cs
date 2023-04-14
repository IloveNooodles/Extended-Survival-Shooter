using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IEnemy
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
    private GameObject player;
    private PlayerQuest playerQuest;

    bool isDead;
    bool isSinking;
    
    public int Id { get; set; }

    void Awake()
    {
        //Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        player = GameObject.FindWithTag("Player");
        playerQuest = player.GetComponent<PlayerQuest>();
        
        //Set current health
        currentHealth = startingHealth;
        healthBarLength = healthBar.rectTransform.rect.width;

        string enemyName = this.gameObject.name.ToLower();
        if (enemyName.Contains(EnemyName.Zombunny))
        {
            Id = EnemyName.GetEnemyId(EnemyName.Zombunny);
        } else if (enemyName.Contains(EnemyName.Zombear))
        {
            Id = EnemyName.GetEnemyId(EnemyName.Zombear);
        }
        else if (enemyName.Contains(EnemyName.Hellepant))
        {
            Id = EnemyName.GetEnemyId(EnemyName.Hellepant);
        }
        else if (enemyName.Contains(EnemyName.Titan))
        {
            Id = EnemyName.GetEnemyId(EnemyName.Titan);
        } else if(enemyName.Contains(EnemyName.Wizard)){
            Id = EnemyName.GetEnemyId(EnemyName.Wizard);
        }

        goldValue += Id;
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

    public virtual void TakeDamage(int amount, Vector3 hitPoint)
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

    public bool IsDead()
    {
        return isDead;
    }

    public virtual void TakeDamage(int amount)
    {
        //Check jika dead
        if (isDead)
            return;

        //play audio
        enemyAudio.Play();
        
        
        currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);
        //Dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
        
        Debug.Log(Id);
    }

    public virtual void Death()
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
        playerQuest.Track(GoalType.Kill, Id, 1);
        playerQuest.Track(GoalType.Spend,  ItemName.ItemId(ItemName.Gold), goldValue);
        Destroy(gameObject, 2.1f);
    }
}