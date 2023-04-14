using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullets : MonoBehaviour
{
    LineRenderer gunLine;

    void Awake()
    {
        gunLine = GetComponent<LineRenderer>();
    }
}
