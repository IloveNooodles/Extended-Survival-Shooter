using UnityEngine;

public class FPSEnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform[] spawnPoints;
    public int maxNumberOfEnemy = 10;

    static public int numberOfEnemy = 0;

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

        if(numberOfEnemy >= maxNumberOfEnemy)
        {
            return;
        }

        while(numberOfEnemy < maxNumberOfEnemy)
        {
            //Mendapatkan nilai random
            int spawnEnemy = Random.Range(0, 3);

            //Memduplikasi enemy
            Factory.FactoryMethod(spawnEnemy, spawnPoints[0].position, spawnPoints[0].rotation);
            numberOfEnemy++;
        }
    }
}
