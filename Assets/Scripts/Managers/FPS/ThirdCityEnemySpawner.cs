using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCityEnemySpawner : MonoBehaviour
{
    public GameObject enemyManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyManager.GetComponent<FPSEnemyManager>().SpawnThirdCity();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyManager.GetComponent<FPSEnemyManager>().SpawnThirdCity();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyManager.GetComponent<FPSEnemyManager>().stopSpawnThirdCity();
        }
    }
}
