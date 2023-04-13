using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if (SceneManager.GetActiveScene().buildIndex == 1)
            // {
            //     SceneManager.LoadScene(2);
            //     
            // }
            // else
            // {
            //     SceneManager.LoadScene(1);
            // }
        }
    }
}
