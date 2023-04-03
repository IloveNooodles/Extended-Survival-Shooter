using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullTitan : MonoBehaviour
{
    public TitanAudio titanAudio;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Roar());
    }

    // Update is called once per frame
    IEnumerator Roar()
    {
        yield return new WaitForSeconds(3f);
        titanAudio.Roar();
    }
}
