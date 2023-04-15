using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedManager : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("toScoreboard", "true");
        // PlayerPrefs.SetFloat("Time", 200f);
        Debug.Log("Time: " + PlayerPrefs.GetFloat("Time", 0f));
        Debug.Log("Tsssiis: " + PlayerPrefs.GetString("toScoreboard", "false"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
