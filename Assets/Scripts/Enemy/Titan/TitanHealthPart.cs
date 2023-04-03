using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanHealthPart : MonoBehaviour
{
    public TitanHealth titanHealth;
    public void TakeDamage(int amount, Vector3 hitPoint, GameObject hitObject)
    {
        titanHealth.TakeDamage(amount, hitPoint, hitObject);
    }
}
