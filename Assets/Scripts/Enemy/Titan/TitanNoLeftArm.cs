using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanNoLeftArm : MonoBehaviour
{
    Animator anim;
    public TitanAudio titanAudio;
    TitanAttackAndMovement titanAttackAndMovement;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        titanAttackAndMovement = GetComponent<TitanAttackAndMovement>();
        StartCoroutine(GetUp());
    }

    IEnumerator GetUp()
    {
        anim.SetTrigger("GetUp");
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("Roar");
        yield return new WaitForSeconds(3f);
        titanAudio.Roar();
        yield return new WaitForSeconds(5f);
        titanAttackAndMovement.enabled = true;
    }
}
