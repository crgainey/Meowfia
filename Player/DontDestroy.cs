using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] gm = GameObject.FindGameObjectsWithTag("GameController");
        GameObject[] UI = GameObject.FindGameObjectsWithTag("UI");
        GameObject[] eventSystem = GameObject.FindGameObjectsWithTag("EventSystem");


        //makes sure there is only one of the gameobject in the scene
        if (gm.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (UI.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (eventSystem.Length > 1)
        {
            Destroy(this.gameObject);
        }


        DontDestroyOnLoad(this.gameObject);
    }
}
