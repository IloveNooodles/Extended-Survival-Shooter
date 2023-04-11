using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public string weaponName = "Sword";
    public int damage = 10;
    public float attackSpeed = 1f;

    public void Attack()
    {
        Debug.Log(weaponName + " Attack");
    }
}
