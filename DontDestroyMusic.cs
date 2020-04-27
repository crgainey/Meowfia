using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    void Awake()
    {

        GameObject[] menuMusic = GameObject.FindGameObjectsWithTag("MenuMusic");

        if (menuMusic.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

  
}
