using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairballActive : MonoBehaviour
{
    GameObject hairballImage;

    void Start()
    {
        //hairballObject.SetActive(true);
        hairballImage = GameObject.FindWithTag("Hairball");

        if (hairballImage != null)
        {
            HairballActivator();
        }
        if (hairballImage == null)
        {
            Debug.Log("Cannot find Hairball");
        }
    }
    void HairballActivator()
    {
        hairballImage.SetActive(true);
    }
}
