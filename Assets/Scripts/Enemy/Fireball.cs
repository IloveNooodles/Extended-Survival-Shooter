using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public WizardAttack wizardAttack;
    public int fireballDamage = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(fireballDamage);
        }
        
        gameObject.SetActive(false);
        wizardAttack.isFireballBeingShooted = false;
    }
}
