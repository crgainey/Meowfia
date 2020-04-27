using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePickup : MonoBehaviour
{
    public AudioSource mouse;

    GameManager _gameManager;
    
    void Start()
    {
        //Finds the game manager script
        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
        {
            _gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        if (gameManagerObject == null)
        {
            Debug.Log("Cannot find 'GameManager' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _gameManager.UpdateMice();
        _gameManager.UpdateTotalMice();
        mouse.Play();
        Destroy(gameObject);
    }
}
