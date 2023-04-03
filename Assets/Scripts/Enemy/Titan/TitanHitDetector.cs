using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanHitDetector : MonoBehaviour
{
    public TitanAttackAndMovement titanAttackAndMovement;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            titanAttackAndMovement.HitPlayer();
        }
    }
}
