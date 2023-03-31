using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCore : MonoBehaviour
{
    public float centerOfGravity = 5.5f;
    Vector3 velocity;
    float translateMultiplier = 0.1f;
    void FixedUpdate()
    {
        //Move up and down
        if(transform.position.y > centerOfGravity)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime * translateMultiplier;
        }
        else
        {
            velocity.y += -1 * Physics.gravity.y * Time.deltaTime * translateMultiplier;
        }
        transform.position += velocity * Time.deltaTime;

        //Rotate
        transform.Rotate(0, 1, 0);
    }
}
