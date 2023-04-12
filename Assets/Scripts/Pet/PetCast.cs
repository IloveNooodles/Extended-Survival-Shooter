using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PetCast : MonoBehaviour
{
    public float castTime = 1f;
    public GameObject castPrefab;


    public int attackDamage = 0;
    public float attackRadius = 0;

    public int healEffect = 0;

    public int buffEffect = 0;


    NavMeshAgent nav;
    private Animator anim;

    private GameObject player;

    private GameObject[] enemies;
    // Start is called before the first frame update


    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Cast", 0, castTime);
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
                GameObject cast = Instantiate(castPrefab, closestEnemy.transform.position, Quaternion.identity);

                // get particle time
                ParticleSystem ps = cast.GetComponent<ParticleSystem>();
                float totalDuration = ps.main.duration;
                
                // damage enemy
                EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();
            
                if (enemyHealth)
                {
                    /* kalo udah mati biarin */
                    if (enemyHealth.IsDead())
                    {
                        return;
                    }
                
                    /* Lakukan Take Damage */
                    enemyHealth.TakeDamage(attackDamage);
                    // TODO: integrasi dengan quest
                    // if (enemyHealth.IsDead())
                    // {
                    //     pq.Track(GoalType.Kill, enemyHealth.Id, 1);
                    // }
                }

                // destroy after particle time
                Destroy(cast, totalDuration);
            }
        }
        else
        {
            Transform playerPosition = player.transform;
            // cast to player
            GameObject cast = Instantiate(castPrefab, player.transform.position, Quaternion.identity);
            // attach to player
            cast.transform.parent = player.transform;

            // get particle time
            ParticleSystem ps = cast.GetComponent<ParticleSystem>();
            float totalDuration = ps.main.duration;
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Heal(healEffect);
            
            
            // TODO: buff damage waiting for weapon system
            // PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
            // playerShooting.setBuffDamage(buffEffect);

            // destroy after particle time
            Destroy(cast, totalDuration);
        }
    }
}