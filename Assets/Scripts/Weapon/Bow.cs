using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    const int MAX_NUM_BULLET = 30;
    public int damage = 10;
    public int buffDamage = 0;
    public float attackSpeed = 1f;
    public float reloadTime = 2f;
    public float range = 100f;

    public string weaponName { get; set; }
    public int numberOfBullets { get; set; }

    public void Awake()
    {
        weaponName = "Bow";
        numberOfBullets = 30;
    }

    public void Attack()
    {
        Debug.Log(weaponName + " Attack");
        numberOfBullets--;
    }

    public void Reload()
    {
        Debug.Log(weaponName + " Reload");
        numberOfBullets = 30;
    }

    public void setBuffDamage(int buffDamage)
    {
        this.buffDamage = buffDamage;
    }
}