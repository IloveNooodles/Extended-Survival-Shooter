using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberOfBulletsManager : MonoBehaviour
{
    public const int MAX_NUM_BULLET = 30;
    public static int numberOfBullets = 0;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        numberOfBullets = 30;
    }
    
    void Update()
    {
        text.text = "Weapon: AK-47\nBullets: " + numberOfBullets + " / 30";
    }
}
