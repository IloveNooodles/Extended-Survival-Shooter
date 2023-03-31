using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCityEnemySpawner : MonoBehaviour
{
    public GameObject enemyManager;
    public Gate gate;

    void Update()
    {
        if(gate.isDestroyed)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyManager.GetComponent<FPSEnemyManager>().SpawnFirstCity();
        }
    }

    void OnTriggerExit(Collider other)
    {
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyManager.GetComponent<FPSEnemyManager>().SpawnFirstCity();
        }
    }
}
