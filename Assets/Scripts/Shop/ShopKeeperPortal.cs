using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopKeeperPortal : MonoBehaviour
{
    bool playerInsidePortal = false;
    private GameObject player;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (playerInsidePortal)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                // player.SetActive(false);
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("lastScene", currentScene);
                SceneManager.LoadScene(4, LoadSceneMode.Additive);
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
