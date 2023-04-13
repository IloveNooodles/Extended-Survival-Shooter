using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberOfBulletsManager : MonoBehaviour
{
    private static NumberOfBulletsManager instance;
    public const int MAX_NUM_BULLET = 30;
    public static int numberOfBullets = 0;
    Text text;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            numberOfBullets = 30;
        }
        
        text = GetComponent<Text>();
    }
    
    void Update()
    {
        text.text = "Bullets: " + numberOfBullets + " / 30";
    }
}
