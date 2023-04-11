using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent nav;
    private Transform player;
    private GameObject[] enemies;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        
         enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 runTo = transform.position + ((transform.position - player.position) * 1);
        // float distance = Vector3.Distance(transform.position, player.position);
        // if (distance < 30) nav.SetDestination(runTo);
        nav.SetDestination(player.position);
        
        // get closest enemy
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject enemy in enemies)
        {
            if(enemy == null) continue;
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistance)
            {
                closestDistance = dSqrToTarget;
                closestEnemy = enemy;
            }
        }
        
        // find distance between pet and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // run away from closest enemy
        if (closestEnemy != null && distanceToPlayer < 3)
        {
            Vector3 runTo = transform.position + ((transform.position - closestEnemy.transform.position) * 1);
            float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);
            
            // print
            Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.red);
            Debug.DrawLine(transform.position, runTo, Color.green);
            
            //print distance and distance to player
            Debug.Log("Distance to player: " + distanceToPlayer);
            Debug.Log("Distance to enemy: " + distance);
            
            
            
            if (distance < 10) nav.SetDestination(runTo);
        }

    }
}
