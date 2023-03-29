using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{

    [SerializeField] public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag, Vector3 position, Quaternion rotation)
    {
        GameObject enemy = Instantiate(enemyPrefab[tag],position,rotation);
        return enemy;
    }
}