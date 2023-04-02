using System.Collections.Generic;
using UnityEngine;

public class FPSEnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform[] firstCitySpawnPoints;
    public Transform[] secondCitySpawnPoints;
    public Transform[] thirdCitySpawnPoints;
    public int FirstCityMaxNumberOfEnemy = 10;
    public int SecondCityMaxNumberOfEnemy = 10;
    public int ThirdCityMaxNumberOfEnemy = 30;

    static public int numberOfEnemy = 0;
    bool isThirdCitySpawnning = false;
    List<GameObject> EnemyMobs = new List<GameObject>();

    [SerializeField] MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    void Start()
    {
    }

    void Update()
    {
        //Mengeksekusi fungs Spawn setiap beberapa detik sesui dengan nilai spawnTime
    }

    public void SpawnFirstCity()
    {
        //Jika player telah mati maka tidak membuat enemy baru
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        if(numberOfEnemy >= FirstCityMaxNumberOfEnemy)
        {
            return;
        }

        while(numberOfEnemy < FirstCityMaxNumberOfEnemy)
        {
            //Mendapatkan nilai random
            int spawnEnemy = Random.Range(0, 3);

            //Memduplikasi enemy
            EnemyMobs.Add(Factory.FactoryMethod(spawnEnemy, firstCitySpawnPoints[0].position, firstCitySpawnPoints[0].rotation));
            numberOfEnemy++;
        }
    }

    public void SpawnThirdCity(){
        if(isThirdCitySpawnning){
            return;
        }
        InvokeRepeating("SpawnThirdCityLogic", 0, 3);
        isThirdCitySpawnning = true;
    }

    public void stopSpawnThirdCity(){
        if(!isThirdCitySpawnning){
            return;
        }
        CancelInvoke("SpawnThirdCityLogic");
        isThirdCitySpawnning = false;
    }

    void SpawnThirdCityLogic(){
        if(numberOfEnemy >= ThirdCityMaxNumberOfEnemy)
        {
            return;
        }
        int spawnEnemy = Random.Range(0, 3);
        float xOffSet = Random.Range(0, 7);
        float zOffSet = Random.Range(-14, 14);
        Vector3 spawnPosition = new Vector3(thirdCitySpawnPoints[0].position.x + xOffSet, thirdCitySpawnPoints[0].position.y, thirdCitySpawnPoints[0].position.z + zOffSet);
        EnemyMobs.Add(Factory.FactoryMethodLimitedEnemy(spawnEnemy, spawnPosition, thirdCitySpawnPoints[0].rotation));
        numberOfEnemy++;
    }

    public void KillAllEnemy()
    {
        CancelInvoke("SpawnThirdCityLogic");
        foreach(GameObject enemy in EnemyMobs)
        {
            try
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(int.MaxValue, Vector3.zero);
            }
            catch
            {
                continue;
            }
        }
    }
}
