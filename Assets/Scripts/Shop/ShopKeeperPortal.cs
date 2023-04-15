using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
