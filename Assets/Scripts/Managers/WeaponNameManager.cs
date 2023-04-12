using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponNameManager : MonoBehaviour
{
    public static string weaponName;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        weaponName = "AK-47";
    }

    void Update()
    {
        text.text = "Weapon: " + weaponName;
    }
}
