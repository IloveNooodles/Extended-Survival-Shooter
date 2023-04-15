using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAvoidingEnemy = true;

    private NavMeshAgent nav;
    private Transform player;
    private Transform riggedBody;
    private GameObject[] enemies;

    Animator anim;

    private float thresholdPlayerDistance = 15f;
    private float thresholdEnemyDistance = 30f;
    private float offsetVector = 5f;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //first child;
        riggedBody = transform.GetChild(0).transform;
        // Debug.Log(riggedBody);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("isOnNavMesh: " + nav.isOnNavMesh);
        if(nav.isOnNavMesh)
        {
            if (isAvoidingEnemy)
            {
                AvoidEnemy();
            }
            else
            {
                nav.SetDestination(player.position);
            }
            
            if (nav.velocity.magnitude < 0.1f)
            {
                // change animation to idle
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }

        }
        else
        {
            Vector3 initial = new Vector3(3.349f,0,-19.61f);
            NavMeshHit hit;
            Vector3 randomPoint = initial + Random.insideUnitSphere * 1f;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
            }
            nav.Warp(finalPosition);
            // transform.position = finalPosition;
        }
        
        
        

    }

    void AvoidEnemy()
    {
        // get closest enemy
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

        // find distance between pet and player
        float distanceToPlayer = (player.position - currentPosition).sqrMagnitude;


        // run away from closest enemy
        if (closestEnemy != null && distanceToPlayer < thresholdPlayerDistance)
        {
            Vector3 runTo = transform.position +
                            ((transform.position - closestEnemy.transform.position) * offsetVector);
            // log runTo
            // log closest distance

            if (closestDistance < thresholdEnemyDistance)
            {
                // Look at
                transform.LookAt(runTo);
                riggedBody.rotation.eulerAngles.Set(riggedBody.rotation.x, transform.rotation.y, riggedBody.rotation.z);
                if (transform.name == "Holy Tree")
                {
                    riggedBody.localEulerAngles = new Vector3(riggedBody.localEulerAngles.x, transform.localEulerAngles.y + 110, riggedBody.localEulerAngles.z);
                }
                else
                {
                    riggedBody.localEulerAngles = new Vector3(riggedBody.localEulerAngles.x, transform.localEulerAngles.y, riggedBody.localEulerAngles.z);
                }
                nav.SetDestination(runTo);
            }
            else
            {
                // just stop there so its avoid pet to oscillate between player and enemy
                // transform.LookAt(transform.position);
                nav.SetDestination(transform.position);
            }
        }
        else
        {
            transform.LookAt(player.position);
            if (transform.name == "Holy Tree")
            {
                riggedBody.localEulerAngles = new Vector3(riggedBody.localEulerAngles.x, transform.localEulerAngles.y + 110, riggedBody.localEulerAngles.z);
            }
            else
            {
                riggedBody.localEulerAngles = new Vector3(riggedBody.localEulerAngles.x, transform.localEulerAngles.y, riggedBody.localEulerAngles.z);
            }
            nav.SetDestination(player.position);
        }
    }
}