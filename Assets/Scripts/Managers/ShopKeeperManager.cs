using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopKeeperManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform shopKeeperSpawnPoint;
    public GameObject shopKeeperPrefab;

    public void Awake()
    {
        // int lastScene = PlayerPrefs.GetInt("lastScene");
        // if (lastScene == 4)
        // {
        //     Spawn();
        // }
    }

    public void Spawn()
    {
        GameObject shopKeeperInScene = GameObject.FindWithTag("ShopKeeper");
        if (shopKeeperInScene == null)
        {
            GameObject shopkeeper = Instantiate(shopKeeperPrefab, shopKeeperSpawnPoint.position,
                shopKeeperSpawnPoint.rotation);
            shopkeeper.tag = "ShopKeeper";
            shopkeeper.gameObject.SetActive(true);
        }
    }
}