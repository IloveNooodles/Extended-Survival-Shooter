using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
