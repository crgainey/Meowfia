using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelThree : MonoBehaviour
{
    GameObject hairball;

    void Start()
    {
        hairball = GameObject.FindGameObjectWithTag("Hairball");
    }
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level 3");
        hairball.SetActive(true);
    }
}
