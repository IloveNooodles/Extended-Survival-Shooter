using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedManager : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("toScoreboard", "true");

        StartCoroutine(ToLeaderboard());
    }

    IEnumerator ToLeaderboard()
    {
        yield return new WaitForSeconds(5f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
