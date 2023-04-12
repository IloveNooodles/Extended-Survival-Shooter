using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PetCast : MonoBehaviour
{
    public float castTime = 1f;
    public GameObject castPrefab;
    public GameObject auraPrefab;


    public int attackDamage = 0;
    public float attackRadius = 0;

    public int healEffect = 0;

    public int buffEffect = 0;


    NavMeshAgent nav;
    private Animator anim;

    private GameObject player;
    private GameObject[] enemies;
    
    float castEndTime = 0f;
    float auraEndTime = 0f;
    // Start is called before the first frame update


    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        
        ParticleSystem psCast = castPrefab.GetComponent<ParticleSystem>();
        ParticleSystem psAura = auraPrefab.GetComponent<ParticleSystem>();
        
        castEndTime = psCast.main.duration;
        auraEndTime = psAura.main.duration;
        
        anim = GetComponent<Animator>();

        if (castTime < 0.1f)
        {
            Transform playerPosition = player.transform;
            // cast to player
            GameObject cast = Instantiate(castPrefab, playerPosition.position, Quaternion.identity);
            GameObject aura = Instantiate(auraPrefab, transform.position, Quaternion.identity);

            aura.transform.parent = transform;
            cast.transform.parent = playerPosition;
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Heal(healEffect);
            
            
            // TODO: buff damage waiting for weapon system
            // PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
            // playerShooting.setBuffDamage(buffEffect);
            
        }
        else
        {
            InvokeRepeating("Cast", 0, castTime);
        }
    }

    void Cast()
    {
        if (attackDamage != 0)
        {
            // find nearest enemy
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject enemy in enemies)
            {
                if (enemy == null) continue;
                Vector3 directionToTarget = enemy.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistance)
                {
                    closestDistance = dSqrToTarget;
                    closestEnemy = enemy;
                }
            }

            // cast spell
            if (closestEnemy != null)
            {
                EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();
            
                if (enemyHealth)
                {
                    /* kalo udah mati biarin */
                    if (!enemyHealth.IsDead())
                    {
                        GameObject cast = Instantiate(castPrefab, closestEnemy.transform.position, Quaternion.identity);
                        GameObject aura = Instantiate(auraPrefab, transform.position, Quaternion.identity);
                        
                        anim.SetTrigger("Cast");

                        aura.transform.parent = transform;


                        /* Lakukan Take Damage */
                        enemyHealth.TakeDamage(attackDamage);
                        // TODO: integrasi dengan quest
                        // if (enemyHealth.IsDead())
                        // {
                        //     pq.Track(GoalType.Kill, enemyHealth.Id, 1);
                        // }
                        Destroy(cast, castEndTime);
                        Destroy(aura, auraEndTime);
                        
                    }
                }
            }
        }
        else
        {
            Transform playerPosition = player.transform;
            // cast to player
            GameObject cast = Instantiate(castPrefab, playerPosition.position, Quaternion.identity);
            GameObject aura = Instantiate(auraPrefab, transform.position, Quaternion.identity);

            aura.transform.parent = transform;
            cast.transform.parent = playerPosition;
            
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Heal(healEffect);
            
            anim.SetTrigger("Cast");
            // TODO: buff damage waiting for weapon system
            // PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
            // playerShooting.setBuffDamage(buffEffect);

            // destroy after particle time
            Destroy(cast, castEndTime);
            Destroy(aura, auraEndTime);
        }
    }
}