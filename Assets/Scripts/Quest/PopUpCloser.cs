using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpCloser : MonoBehaviour
{
    public GameObject popupQuest;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(closePopup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator closePopup()
    {
        yield return new WaitForSeconds(0.5f);
        popupQuest.SetActive(false);
        Destroy(gameObject);
    }
}
