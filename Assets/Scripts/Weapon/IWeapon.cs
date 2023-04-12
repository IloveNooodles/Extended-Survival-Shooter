using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string weaponName { get; set; }
    int numberOfBullets { get; set; }

    void Attack();
    void Reload();
    void setBuffDamage(int buffDamage);
}
