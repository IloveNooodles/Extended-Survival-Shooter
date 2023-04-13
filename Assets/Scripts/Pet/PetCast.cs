using System;
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
    public float AOERadius = 0;

    public int healEffect = 0;

    public int buffEffect = 0;
    



    private Animator anim;

    private GameObject player;
    private GameObject[] enemies;
    
    float castEndTime = 0f;
    float auraEndTime = 0f;
    
    
    GameObject permanentAura;
    GameObject permanentCast;
    

    void OnEnable()
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
            permanentCast = Instantiate(castPrefab, playerPosition.position, Quaternion.identity);
            permanentAura = Instantiate(auraPrefab, transform.position, Quaternion.identity);

            permanentAura.transform.parent = transform;
            permanentCast.transform.parent = playerPosition;
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Heal(healEffect);


            if (WeaponManager.currentWeapon != null)
            {
                WeaponManager.currentWeapon.setBuffDamage(buffEffect);
            }
            
        }
        else
        {
            InvokeRepeating("Cast", 2f, castTime);
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
            if (closestEnemy != null && closestDistance < attackRadius)
            {
                EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();
            
                if (enemyHealth)
                {
                    /* kalo udah mati biarin */
                    if (!enemyHealth.IsDead())
                    {
                        
                        GameObject cast = Instantiate(castPrefab, closestEnemy.transform.position, Quaternion.identity);
                        GameObject aura = Instantiate(auraPrefab, transform.position, Quaternion.identity);
                        
                        // change look at to enemy
                        transform.LookAt(closestEnemy.transform);
                        
                        anim.SetTrigger("Cast");
                        


                        aura.transform.parent = transform;


                        /* Lakukan Take Damage */
                        
                        // enemyHealth.TakeDamage(attackDamage);
                        foreach (GameObject enemy in enemies)
                        {
                            if (enemy == null) continue;
                            Vector3 directionToTarget = enemy.transform.position - closestEnemy.transform.position;
                            float dSqrToTarget = directionToTarget.sqrMagnitude;
                            
                            if (dSqrToTarget < AOERadius)
                            {
                                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                                health.TakeDamage(attackDamage);
                            }
                        }
                        
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
            
            

            // destroy after particle time
            Destroy(cast, castEndTime);
            Destroy(aura, auraEndTime);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
        Destroy(permanentCast);
        Destroy(permanentAura);
    }
}