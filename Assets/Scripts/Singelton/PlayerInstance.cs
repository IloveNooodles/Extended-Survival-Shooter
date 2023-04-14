using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInstance : MonoBehaviour
{
    private static PlayerInstance _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                gameObject.transform.position = new Vector3(0,0,0);
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
