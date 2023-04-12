using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public string weaponName = "Bow";
    public int damage = 10;
    public float attackSpeed = 1f;
    
    
    int buffDamage = 0;

    public void Attack()
    {
        Debug.Log(weaponName + " Attack");
    }
    
    public void setBuff(int buffDamage)
    {
        this.buffDamage = buffDamage;
    }
}