using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    public string weaponName = "Gun";
    public int damage = 10;
    public float attackSpeed = 1.0f;

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
