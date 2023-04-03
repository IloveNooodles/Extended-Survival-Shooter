using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullTitan : MonoBehaviour
{
    public TitanAudio titanAudio;
    TitanAttackAndMovement titanAttackAndMovement;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        titanAttackAndMovement = GetComponent<TitanAttackAndMovement>();
        anim.SetTrigger("Roar");
        StartCoroutine(Roar());
    }

    // Update is called once per frame
    IEnumerator Roar()
    {
        yield return new WaitForSeconds(3f);
        titanAudio.Roar();
        yield return new WaitForSeconds(5f);
        titanAttackAndMovement.enabled = true;
    }
}
