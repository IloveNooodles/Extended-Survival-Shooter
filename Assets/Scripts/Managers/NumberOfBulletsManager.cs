using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberOfBulletsManager : MonoBehaviour
{
    public static int numberOfBullets = 30;
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
