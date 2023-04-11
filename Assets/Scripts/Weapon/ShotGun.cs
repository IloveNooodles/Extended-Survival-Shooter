using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour, IWeapon
{
    public string weaponName = "ShotGun";
    public int damage = 20;
    public float attackSpeed = 2.0f;
    public float range = 10.0f;

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
