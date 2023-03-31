using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDown : MonoBehaviour, IEnvironment
{
    public bool isClockDown = false;
    public AudioClip fallingClip;

    float fallVelocity = 0f;

    GameObject particleLeft;
    GameObject particleRight;
    GameObject particleDownLeft;
    GameObject particleDownRight;
    AudioSource clockAudio;

    // Start is called before the first frame update
    void Start()
    {
        clockAudio = GetComponent<AudioSource>();
        particleLeft = this.gameObject.transform.GetChild(2).gameObject;
        particleRight = this.gameObject.transform.GetChild(3).gameObject;
        particleDownLeft = this.gameObject.transform.GetChild(4).gameObject;
        particleDownRight = this.gameObject.transform.GetChild(5).gameObject;
    }

    void FixedUpdate()
    {
        if (!isClockDown)
        {
            return;
        }
        else
        {
            if (transform.position.y > 0f)
            {
                //simulate gravity
                Vector3 pos = transform.position;
                fallVelocity += Physics.gravity.y * Time.deltaTime;
                pos.y += fallVelocity * Time.deltaTime;
                transform.position = pos;
            }else{
                particleDownLeft.SetActive(true);
                particleDownRight.SetActive(true);
            }
        }
    }

    public void Death(){
        if(isClockDown){
            return;
        }
        
        isClockDown = true;

        //Play falling audio
        clockAudio.clip = fallingClip;
        clockAudio.Play();

        particleLeft.SetActive(true);
        particleRight.SetActive(true);

        Destroy(particleLeft, 2.5f);
        Destroy(particleRight, 2.5f);
    }
}
