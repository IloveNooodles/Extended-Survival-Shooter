using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static int Gold;
    [SerializeField] private Text text;

    private void Awake()
    {
        Gold = 0;
    }

    private void Update()
    {
        text.text = "Gold: " + Gold;
    }
}
