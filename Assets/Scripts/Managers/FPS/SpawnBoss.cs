using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    public GameObject ThirdCityEnemySpawner;
    public GameObject enemyManager;
    public GameObject[] spawnGates;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(SpawnBossLogic());
        }
    }

    IEnumerator SpawnBossLogic()
    {
        enemyManager.GetComponent<FPSEnemyManager>().stopSpawnThirdCity();
        Destroy(ThirdCityEnemySpawner);
        boss.SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyManager.GetComponent<FPSEnemyManager>().KillAllEnemy();
        foreach (GameObject gate in spawnGates)
        {
            gate.GetComponent<Gate>().Death();
        }
        Destroy(gameObject);
    }
}
