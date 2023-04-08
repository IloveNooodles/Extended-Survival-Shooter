using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableTallHouse : MonoBehaviour
{
    public GameObject rubble;
    public ParticleSystem smoke;
    bool isTallHouseBroken = false;

    // Update is called once per frame
    void Update()
    {
        if (isTallHouseBroken)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * 6);
            if(gameObject.transform.position.y < -25)
            {
                rubble.SetActive(true);
                isTallHouseBroken = false;
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Jika Terkena Titan
        if(collision.gameObject.layer == 7)
        {
            isTallHouseBroken = true;
            smoke.Play();
        }
    }
}
