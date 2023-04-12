using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour, IWeapon
{
    public string weaponName = "ShotGun";
    public int damage = 30;
    public float attackSpeed = 1f;
    
    private int buffDamage = 0;

    public void Attack()
    {
        Debug.Log(weaponName + " Attack");
    }
    
    public void setBuff(int buffDamage)
    {
        this.buffDamage = buffDamage;
    }
}
