using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupShopManager : MonoBehaviour
{
    private int currentScene;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OpenShopButton()
    {
        // player.SetActive(false);
        currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("lastScene", currentScene);
        SceneManager.LoadScene(4);
    }


}
