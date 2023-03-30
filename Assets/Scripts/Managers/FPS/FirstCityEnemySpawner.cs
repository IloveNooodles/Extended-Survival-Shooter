using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCityEnemySpawner : MonoBehaviour
{
    public GameObject enemyManager;
    public ClockDown clock;

    void Update()
    {
        if(clock.isClockDown)
        {
            Debug.Log("Clock Down");
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
