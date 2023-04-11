using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public string weaponName = "Sword";
    public int damage = 10;
    public float attackSpeed = 2.0f;

    public void Start()
    {
        Debug.Log(weaponName + " start!");
        Attack();
    }

    public void Attack()
    {
        Debug.Log(weaponName + " attack with " + damage + " damage!");
    }
}
