using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCityEnemySpawner : MonoBehaviour
{
    public GameObject enemyManager;
    public Transform clock;

    void Update()
    {
        //if clock have been knocked out, remove this object
        if(clock.position.y == 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Enter");
            enemyManager.GetComponent<FPSEnemyManager>().SpawnFirstCity();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyManager.GetComponent<FPSEnemyManager>().SpawnFirstCity();
        }
    }
}
