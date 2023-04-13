using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public int damage = 5;
    public int buffDamage = 0;
    public float attackSpeed = 1f;

    public string weaponName { get; set; }
    public int numberOfBullets { get; set; }

    public void Awake()
    {
        weaponName = "Sword";
        numberOfBullets = 0;
    }

    public void Attack()
    {
        Debug.Log(weaponName + " Attack");
    }

    public void Reload()
    {
        Debug.Log(weaponName + " Reload");
    }

    public void setBuffDamage(int buffDamage)
    {
        this.buffDamage = buffDamage;
    }
}
