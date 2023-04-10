using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
     {
         if (Input.GetMouseButtonUp(0))
         {
             EventSystem.current.SetSelectedGameObject(null);
         }
     }
}
