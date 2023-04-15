using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour, IWeapon
{
    const int MAX_NUM_BULLET = 30;
    public int damage = 30;
    public int buffDamage = 0;
    public float attackSpeed = 1f;
    public float reloadTime = 2f;
    public float range = 10f;
    public int bulletsPerShot = 5;

    public string weaponName { get; set; }
    public int numberOfBullets { get; set; }

    public void Awake()
    {
        weaponName = "ShotGun";
        numberOfBullets = 30;
    }

    public void Attack()
    {
        // Debug.Log(weaponName + " Attack");
        numberOfBullets -= bulletsPerShot;
    }

    public void Reload()
    {
        // Debug.Log(weaponName + " Reload");
        numberOfBullets = 30;
    }

    public void setBuffDamage(int buffDamage)
    {
        this.buffDamage = buffDamage;
    }
}
