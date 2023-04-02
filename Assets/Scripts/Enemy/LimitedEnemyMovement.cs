using System.Collections;
using UnityEngine;

public class LimitedEnemyMovement : MonoBehaviour
{
    public float detectionRange = 30f;
    public float chaseRange = 50f;
    bool isChasing = false;
    GameObject player;
    Vector3 initialPosition;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Set initial position
        initialPosition = transform.position;

        //Mendapatkan componen reference
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //Jika lagi idle cek apakah player masuk detection range
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange)

            {
                anim.SetTrigger("Move");
                isChasing = true;
                nav.enabled = true;
            }
        }

        //Jika lagi chasing cek apakah player sudah keluar dari chase range
        if (isChasing)
        {
            if (Vector3.Distance(transform.position, initialPosition) >= chaseRange)
            {
                isChasing = false;
                nav.enabled = false;
                anim.SetTrigger("Idle");
                StartCoroutine("returnToInitialPosition");
            }
        }

        //Pindah ke player position
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (isChasing)
            {
                nav.SetDestination(player.transform.position);
            }
        }
        else //Stop moving
        {
            nav.enabled = false;
        }
    }

    IEnumerator returnToInitialPosition()
    {
        yield return new WaitForSeconds(3f);
        transform.position = initialPosition;
    }
}