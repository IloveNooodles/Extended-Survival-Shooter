using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public string weaponName = "Bow";
    public int damage = 10;
    public float attackSpeed = 1f;

    public void Attack()
    {
        Debug.Log(weaponName + " Attack");
    }
}