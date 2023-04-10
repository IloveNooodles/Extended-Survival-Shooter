using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    private Transform pet;

    void Awake()
    {
        //Cari game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Cari game object with tag pet
        if (GameObject.FindGameObjectWithTag("Pet")!= null)
        {
            pet = GameObject.FindGameObjectWithTag("Pet").transform;
        }

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
            nav.SetDestination(player.position);
            
            if (pet != null)
            {
                // find the closest distance between pet and player
                float distanceToPlayer = (player.position - transform.position).sqrMagnitude;
                float distanceToPet = (pet.position - transform.position).sqrMagnitude;
                
                if(distanceToPlayer < distanceToPet)
                    nav.SetDestination(player.position);
            }
        }
        else //Stop moving
        {
            nav.enabled = false;
        }
    }
}