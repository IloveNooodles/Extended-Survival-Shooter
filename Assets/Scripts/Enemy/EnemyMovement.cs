using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        //Cari game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        //Mendapatkan componen reference
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }



    void Update()
    {
        //Pindah ke player position
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (PetManager.currentPet != null && PetManager.currentPet.activeInHierarchy)
            {
                // find the closest distance between pet and player
                float distanceToPlayer = (player.position - transform.position).sqrMagnitude;
                float distanceToPet = (PetManager.currentPet.transform.position - transform.position).sqrMagnitude;

                if (distanceToPlayer > distanceToPet)
                {
                    nav.ResetPath();
                    nav.SetDestination(PetManager.currentPet.transform.position);
                }
                else
                {
                    nav.ResetPath();
                    nav.SetDestination(player.position);
                }
            }
            else
            {
                nav.ResetPath();
                nav.SetDestination(player.position);
            }
        }
        else //Stop moving
        {
            nav.enabled = false;
        }
    }
}