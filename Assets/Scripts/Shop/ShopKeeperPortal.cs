using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopKeeperPortal : MonoBehaviour
{
    bool playerInsidePortal = false;
    private GameObject player;
    private bool isOpenShop = false;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (playerInsidePortal && !isOpenShop)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {

                int currentScene = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("lastScene", currentScene);
                SceneManager.LoadScene(4);
                isOpenShop = true;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        

        if (other.gameObject.CompareTag("Player"))
        {
            playerInsidePortal = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsidePortal = false;
        }
    }
    
}
